import type { BachelorPad } from './types';

export const API_BASE_URL = 'http://mohamed.homeip.net:8055';

export async function fetchBachelorPads(): Promise<BachelorPad[]> {
  const response = await fetch(`${API_BASE_URL}/bachelorpad`);
  if (!response.ok) {
    throw new Error(`Failed to load apartments (${response.status})`);
  }
  return response.json();
}
export async function updateBachelorPadStatus(
  id: number,
  payload: { status: string; observation?: string | null }
): Promise<void> {
  const response = await fetch(`${API_BASE_URL}/bachelorpad/${id}/status`, {
    method: 'PATCH',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(payload),
  });
  if (response.status === 204) return;
  if (response.status === 400) throw new Error('Invalid status');
  if (response.status === 404) throw new Error('Apartment not found');
  if (!response.ok) throw new Error(`Failed to update status (${response.status})`);
}
