import { useCallback, useEffect, useRef, useState } from 'react';
import { fetchBachelorPads } from '../api';
import type { BachelorPad } from '../types';

interface UseBachelorPadsResult {
  apartments: BachelorPad[];
  loading: boolean;
  error: string | null;
  refresh: () => Promise<void>;
}

export function useBachelorPads(): UseBachelorPadsResult {
  const [apartments, setApartments] = useState<BachelorPad[]>([]);
  const apartmentsRef = useRef(apartments);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    apartmentsRef.current = apartments;
  }, [apartments]);

  const refresh = useCallback(async () => {
    try {
      setLoading(true);
      setError(null);
      const data = await fetchBachelorPads();
      setApartments(data);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Unexpected error');
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    void refresh();
  }, [refresh]);

  return {
    apartments,
    loading,
    error,
    refresh,
  };
}
