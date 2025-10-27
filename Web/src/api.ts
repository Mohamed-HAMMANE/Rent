import type { BachelorPad } from './types';

export const API_BASE_URL = 'http://mohamed.homeip.net:8055';

export async function fetchBachelorPads(): Promise<BachelorPad[]> {
  const response = await fetch(`${API_BASE_URL}/bachelorpad`);
  if (!response.ok) {
    throw new Error(`Failed to load apartments (${response.status})`);
  }
  return response.json();
}
