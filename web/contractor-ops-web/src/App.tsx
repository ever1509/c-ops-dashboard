import { useEffect, useState } from "react";
import { getClients, Client } from "./api/clientsApi";

function App() {
  const [clients, setClients] = useState<Client[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    getClients()
      .then(setClients)
      .catch(() => setError("Failed to load clients"))
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <p>Loading...</p>;
  if (error) return <p>{error}</p>;
  if (clients.length === 0) return <p>No clients yet</p>;

  return (
    <div style={{ padding: "1rem" }}>
      <h1>Clients</h1>
      <table border={1} cellPadding={8}>
        <thead>
          <tr>
            <th>Name</th>
            <th>Website</th>
            <th>Created</th>
          </tr>
        </thead>
        <tbody>
          {clients.map(c => (
            <tr key={c.id}>
              <td>{c.name}</td>
              <td>{c.website ?? "-"}</td>
              <td>{new Date(c.createdAt).toLocaleDateString()}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default App;
