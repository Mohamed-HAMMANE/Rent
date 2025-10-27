import { useMemo, useRef, useCallback, useState } from 'react';
import type { ReactNode, KeyboardEvent, ChangeEvent } from 'react';
import './App.css';
import { API_BASE_URL } from './api';
import { useBachelorPads } from './hooks/useBachelorPads';
import type { BachelorPad } from './types';

type RatingMetric = 'quality' | 'location' | 'aesthetics' | 'furniture';

const RATING_FIELDS: Array<{
  key: RatingMetric;
  label: string;
  icon: ReactNode;
}> = [
  {
    key: 'quality',
    label: 'Quality',
    icon: (
      <svg viewBox="0 0 20 20" aria-hidden="true">
        <path
          d="M10 2.5 12 7l4.8.35-3.7 2.93 1.12 4.72L10 12.9 5.78 15l1.12-4.72L3.2 7.35 8 7z"
          fill="currentColor"
        />
      </svg>
    ),
  },
  {
    key: 'location',
    label: 'Location',
    icon: (
      <svg viewBox="0 0 20 20" aria-hidden="true">
        <path
          d="M10 2.5a5.25 5.25 0 0 0-5.25 5.25c0 3.64 4.67 8.87 4.87 9.08.2.22.56.22.76 0 .2-.21 4.87-5.44 4.87-9.08A5.25 5.25 0 0 0 10 2.5Zm0 7.5a2.25 2.25 0 1 1 0-4.5 2.25 2.25 0 0 1 0 4.5Z"
          fill="currentColor"
        />
      </svg>
    ),
  },
  {
    key: 'aesthetics',
    label: 'Aesthetics',
    icon: (
      <svg viewBox="0 0 20 20" aria-hidden="true">
        <path
          d="M12 2a8 8 0 1 0 0 16h.5a1.5 1.5 0 0 0 0-3H12a1 1 0 0 1 0-2h1.75a2.75 2.75 0 0 0 .28-5.5A2.75 2.75 0 0 0 12 2Zm-4.5 5.25a1 1 0 1 1 0 2 1 1 0 0 1 0-2Zm1.5 4a1 1 0 1 1 0 2 1 1 0 0 1 0-2Zm4-3.5a1 1 0 1 1-2 0 1 1 0 0 1 2 0Zm-1.5-2a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"
          fill="currentColor"
        />
      </svg>
    ),
  },
  {
    key: 'furniture',
    label: 'Furniture',
    icon: (
      <svg viewBox="0 0 20 20" aria-hidden="true">
        <path
          d="M4.25 7A2.25 2.25 0 0 0 2 9.25v4A.75.75 0 0 0 3.5 13v-.75h13V13a.75.75 0 0 0 1.5 0v-3.75A2.25 2.25 0 0 0 15.75 7H4.25Zm0 1.5h11.5c.414 0 .75.336.75.75v1.5H3.5v-1.5c0-.414.336-.75.75-.75Z"
          fill="currentColor"
        />
        <path
          d="M3.5 13.5v1.25a.75.75 0 0 0 1.5 0V13.5h-1.5Zm11.5 0v1.25a.75.75 0 0 0 1.5 0V13.5h-1.5Z"
          fill="currentColor"
        />
      </svg>
    ),
  },
];

const PRICE_CURRENCY_FORMATTER = new Intl.NumberFormat('en-US', {
  style: 'currency',
  currency: 'MAD',
  currencyDisplay: 'code',
  maximumFractionDigits: 0,
});

const PRICE_VALUE_FORMATTER = new Intl.NumberFormat('en-US', {
  maximumFractionDigits: 0,
});

type SortField =
  | 'mark-desc'
  | 'mark-asc'
  | 'price-asc'
  | 'price-desc'
  | 'quality'
  | 'recent';

const SORT_OPTIONS: Array<{ value: SortField; label: string }> = [
  { value: 'mark-desc', label: 'Mark (high → low)' },
  { value: 'mark-asc', label: 'Mark (low → high)' },
  { value: 'price-asc', label: 'Price (low → high)' },
  { value: 'price-desc', label: 'Price (high → low)' },
  { value: 'quality', label: 'Quality score' },
  { value: 'recent', label: 'Recently added' },
];

const FALLBACK_IMAGE = (() => {
  const svg = `
  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 320 200">
    <defs>
      <linearGradient id="g" x1="0" y1="0" x2="1" y2="1">
        <stop offset="0" stop-color="%23243b53"/>
        <stop offset="1" stop-color="%23171f2c"/>
      </linearGradient>
    </defs>
    <rect width="320" height="200" fill="url(%23g)"/>
    <g fill="%23ffffff" fill-opacity="0.75">
      <circle cx="90" cy="85" r="28"/>
      <rect x="130" y="70" width="90" height="12" rx="6"/>
      <rect x="130" y="95" width="70" height="12" rx="6"/>
    </g>
  </svg>`;
  return `data:image/svg+xml;charset=UTF-8,${encodeURIComponent(svg)}`;
})();

function getImageSource(apartment: BachelorPad): string {
  if (apartment.imageUrl) {
    if (apartment.imageUrl.startsWith('http')) {
      return apartment.imageUrl;
    }
    if (apartment.imageUrl.startsWith('/')) {
      return `${API_BASE_URL}${apartment.imageUrl}`;
    }
    return `${API_BASE_URL}/${apartment.imageUrl}`;
  }
  return `${API_BASE_URL}/bachelorpad/${apartment.id}/image`;
}

function analyseNote(note: string | null) {
  if (!note) {
    return null;
  }
  const trimmed = note.trim();
  if (!trimmed) {
    return null;
  }
  const lower = trimmed.toLowerCase();
  if (lower.includes('visit')) {
    return { icon: '📅', tone: 'visit' as const, text: trimmed };
  }
  if (lower.includes('noisy') || lower.includes('noise')) {
    return { icon: '⚠️', tone: 'warn' as const, text: trimmed };
  }
  if (lower.includes('flexible') || lower.includes('owner')) {
    return { icon: '📝', tone: 'info' as const, text: trimmed };
  }
  return { icon: '📝', tone: 'info' as const, text: trimmed };
}

function App() {
  const { apartments, loading, error } = useBachelorPads();
  const cardRefs = useRef<Array<HTMLElement | null>>([]);
  const [sortField, setSortField] = useState<SortField>('mark-desc');

  const hasDistinctAddresses = useMemo(() => {
    const values = new Set(
      apartments
        .map((item) => (item.address ?? '').trim().toLowerCase())
        .filter(Boolean)
    );
    return values.size > 1;
  }, [apartments]);

  const sortedApartments = useMemo(() => {
    return [...apartments].sort((a, b) => {
      switch (sortField) {
        case 'mark-asc': {
          const aMark = typeof a.mark === 'number' ? a.mark : Infinity;
          const bMark = typeof b.mark === 'number' ? b.mark : Infinity;
          return aMark - bMark;
        }
        case 'price-asc':
          return a.price - b.price;
        case 'price-desc':
          return b.price - a.price;
        case 'quality':
          return b.quality - a.quality;
        case 'recent':
          return b.id - a.id;
        case 'mark-desc':
        default: {
          const aMark = typeof a.mark === 'number' ? a.mark : -Infinity;
          const bMark = typeof b.mark === 'number' ? b.mark : -Infinity;
          return bMark - aMark;
        }
      }
    });
  }, [apartments, sortField]);

  const summary = useMemo(() => {
    if (sortedApartments.length === 0) {
      return {
        count: 0,
        topMark: null as number | null,
        averageRent: null as number | null,
      };
    }
    const count = sortedApartments.length;
    const marks = sortedApartments
      .map((item) => item.mark)
      .filter((value): value is number => typeof value === 'number');
    const topMark = marks.length ? Math.max(...marks) : null;
    const averageRent =
      sortedApartments.reduce((sum, item) => sum + item.price, 0) / count;
    return { count, topMark, averageRent };
  }, [sortedApartments]);

  const handleKeyNavigation = useCallback(
    (event: KeyboardEvent<HTMLElement>, index: number) => {
      if (
        event.key !== 'ArrowDown' &&
        event.key !== 'ArrowUp' &&
        event.key !== 'ArrowRight' &&
        event.key !== 'ArrowLeft'
      ) {
        return;
      }
      event.preventDefault();
      const direction =
        event.key === 'ArrowDown' || event.key === 'ArrowRight' ? 1 : -1;
      const nextIndex = index + direction;
      const nextCard = cardRefs.current[nextIndex];
      if (nextCard) {
        nextCard.focus();
      }
    },
    []
  );

  cardRefs.current = cardRefs.current.slice(0, sortedApartments.length);

  return (
    <div className="page">
      <header className="page-header">
        <div className="page-header__left">
          <h1>Apartment shortlist</h1>
        </div>
        <div className="summary-bar summary-bar--inline">
          <div className="summary-item">
            <span className="summary-label">Tracked</span>
            <strong>{summary.count}</strong>
          </div>
          <div className="summary-item">
            <span className="summary-label">Top mark</span>
            <strong>
              {summary.topMark !== null ? summary.topMark.toFixed(2) : '-'}
            </strong>
          </div>
          <div className="summary-item">
            <span className="summary-label">Average rent</span>
            <strong>
              {summary.averageRent !== null
                ? PRICE_CURRENCY_FORMATTER.format(summary.averageRent)
                : '-'}
            </strong>
          </div>
        </div>
      </header>
      <div className="list-controls" role="region" aria-label="List controls">
        <label className="list-controls__label" htmlFor="sort-field">
          Sort by
        </label>
        <select
          id="sort-field"
          className="list-controls__select"
          value={sortField}
          onChange={(event: ChangeEvent<HTMLSelectElement>) =>
            setSortField(event.target.value as SortField)
          }
        >
          {SORT_OPTIONS.map((option) => (
            <option key={option.value} value={option.value}>
              {option.label}
            </option>
          ))}
        </select>
        <span className="list-controls__note">All prices in MAD</span>
      </div>
      {error && (
        <div className="banner banner--error" role="alert">
          {error}
        </div>
      )}
      <section className="apartment-grid" role="list">
        {sortedApartments.map((apartment, index) => {
          const hasVisitScheduled =
            typeof apartment.observation === 'string' &&
            /visit/i.test(apartment.observation);
          const note = analyseNote(apartment.observation);
          const displayName = hasDistinctAddresses
            ? apartment.address || 'Address TBD'
            : `Pad #${apartment.id}`;
          return (
            <article
              key={apartment.id}
              className={`apartment-card${
                hasVisitScheduled ? ' apartment-card--visit' : ''
              }`}
              role="listitem"
              tabIndex={0}
              ref={(node) => {
                cardRefs.current[index] = node;
              }}
              onKeyDown={(event) => handleKeyNavigation(event, index)}
            >
              {hasVisitScheduled && (
                <span className="visit-indicator" aria-label="Visit scheduled" />
              )}
              <div className="apartment-card__top">
                <div className="apartment-thumb">
                  <a
                    className="thumb-link"
                    href={apartment.link}
                    target="_blank"
                    rel="noopener noreferrer"
                    aria-label="Open listing"
                    title="Open listing"
                  >
                    <img
                      src={getImageSource(apartment)}
                      alt={displayName}
                      onError={(event) => {
                        event.currentTarget.onerror = null;
                        event.currentTarget.src = FALLBACK_IMAGE;
                      }}
                      loading="lazy"
                    />
                  </a>
                  {apartment.chatLink && (
                    <a
                      href={apartment.chatLink}
                      target="_blank"
                      rel="noopener noreferrer"
                      className="thumb-chat"
                      aria-label="Open chat"
                      title="Open chat"
                    >
                      <svg viewBox="0 0 16 16" aria-hidden="true">
                        <path d="M6.5 11.5 3 13l.8-3.2A4.5 4.5 0 0 1 3 8.5 4.5 4.5 0 1 1 7.5 13a4.5 4.5 0 0 1-.5-.03l-.5.53Z" fill="none" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round"/>
                        <circle cx="6.75" cy="7" r="0.65" fill="currentColor"/>
                        <circle cx="9" cy="7" r="0.65" fill="currentColor"/>
                        <circle cx="11.25" cy="7" r="0.65" fill="currentColor"/>
                      </svg>
                    </a>
                  )}
                  <span className="mark-badge mark-badge--overlay text-mono">
                    <svg viewBox="0 0 16 16" aria-hidden="true" className="mark-badge__icon">
                      <path d="M8 1.8 9.6 6l4.4.3-3.4 2.7 1 4.1L8 10.8 4.4 13.1l1-4.1L2 6.3 6.4 6z" fill="currentColor"/>
                    </svg>
                    {apartment.mark !== null && apartment.mark !== undefined
                      ? apartment.mark.toFixed(2)
                      : 'N/A'}
                  </span>
                  <span className="thumb-address">{displayName}</span>
                  <span className="thumb-price text-mono">
                    {PRICE_VALUE_FORMATTER.format(apartment.price)} MAD
                  </span>
                </div>
              </div>
              <div className="spec-row" role="list">
                {RATING_FIELDS.map(({ key, label, icon }) => (
                  <div key={key} className="spec" role="listitem" title={`${label}: ${apartment[key]}`}>
                    <span className="spec-icon" aria-hidden="true">{icon}</span>
                    <span className="spec-value text-mono">{apartment[key]}</span>
                    <span className="spec-label">{label}</span>
                  </div>
                ))}
              </div>
              <div
                className="apartment-actions"
                role="group"
                aria-label="Quick actions"
              />
              {note && (
                <div
                  className={`apartment-note apartment-note--${note.tone}`}
                  role="note"
                >
                  <span className="apartment-note__icon" aria-hidden="true">
                    {note.icon}
                  </span>
                  <p>{note.text}</p>
                </div>
              )}
            </article>
          );
        })}
        {!loading && sortedApartments.length === 0 && (
          <div className="empty-state">
            Your list is empty. Once the API serves entries, they will appear here ready for review.
          </div>
        )}
      </section>
    </div>
  );
}

export default App;






