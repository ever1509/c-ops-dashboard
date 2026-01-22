export interface Client {
  id: number;
  name: string;
  website?: string | null;
  notes?: string | null;
  createdAt: string;
}

const BASE_URL = "http://localhost:5000";

export async function getClients(): Promise<Client[]> {
  const res = await fetch(`${BASE_URL}/clients`);
  if (!res.ok) {
    throw new Error("Failed to fetch clients");
  }
  return res.json();
}
