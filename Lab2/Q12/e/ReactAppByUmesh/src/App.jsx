import { useState } from "react";

export default function App() {
  const [currentView, setCurrentView] = useState("home");
  const [num1, setNum1] = useState("");
  const [num2, setNum2] = useState("");
  const [operation, setOperation] = useState("add");
  const [result, setResult] = useState(null);

  const currentYear = new Date().getFullYear();

  const handleCompute = () => {
    if (num1 === "" || num2 === "") {
      setResult(null);
      return;
    }

    const n1 = parseFloat(num1);
    const n2 = parseFloat(num2);

    switch (operation) {
      case "add":
        setResult(n1 + n2);
        break;
      case "subtract":
        setResult(n1 - n2);
        break;
      case "multiply":
        setResult(n1 * n2);
        break;
      default:
        setResult(null);
    }
  };

  return (
    <div
      style={{
        display: "flex",
        flexDirection: "column",
        minHeight: "100vh",
        fontFamily: "sans-serif",
        margin: 0,
      }}
    >
      {/* Navbar */}
      <nav style={{ backgroundColor: "#333", padding: "15px" }}>
        <button
          onClick={() => setCurrentView("home")}
          style={{
            background: "none",
            border: "none",
            color: "white",
            marginRight: "20px",
            cursor: "pointer",
            fontWeight: "bold",
            fontSize: "1em",
          }}
        >
          Home
        </button>
        <button
          onClick={() => setCurrentView("calculator")}
          style={{
            background: "none",
            border: "none",
            color: "white",
            cursor: "pointer",
            fontWeight: "bold",
            fontSize: "1em",
          }}
        >
          Calculator
        </button>
      </nav>

      {/* Main Content Area */}
      <main style={{ flex: 1, padding: "30px", textAlign: "center" }}>
        {currentView === "home" ? (
          <div>
            <h2>Home</h2>
            <img
              src="/me.jpg"
              alt="Your Photo"
              style={{ borderRadius: "8px", maxWidth: "50%", height: "400px" }}
            />
          </div>
        ) : (
          <div
            style={{ maxWidth: "400px", margin: "0 auto", textAlign: "left" }}
          >
            <h2>Calculator</h2>

            <div style={{ marginBottom: "15px" }}>
              <label>Number 1:</label>
              <br />
              <input
                type="number"
                value={num1}
                onChange={(e) => setNum1(e.target.value)}
                style={{
                  width: "100%",
                  padding: "8px",
                  boxSizing: "border-box",
                }}
              />
            </div>

            <div style={{ marginBottom: "15px" }}>
              <label>Number 2:</label>
              <br />
              <input
                type="number"
                value={num2}
                onChange={(e) => setNum2(e.target.value)}
                style={{
                  width: "100%",
                  padding: "8px",
                  boxSizing: "border-box",
                }}
              />
            </div>

            <div style={{ marginBottom: "15px" }}>
              <label>Operation:</label>
              <br />
              <select
                value={operation}
                onChange={(e) => setOperation(e.target.value)}
                style={{
                  width: "100%",
                  padding: "8px",
                  boxSizing: "border-box",
                }}
              >
                <option value="add">Add</option>
                <option value="subtract">Subtract</option>
                <option value="multiply">Multiply</option>
              </select>
            </div>

            <button
              onClick={handleCompute}
              style={{
                padding: "10px 20px",
                backgroundColor: "#0d6efd",
                color: "white",
                border: "none",
                borderRadius: "4px",
                cursor: "pointer",
              }}
            >
              Compute
            </button>

            {result !== null && (
              <div
                style={{
                  marginTop: "20px",
                  fontWeight: "bold",
                  fontSize: "1.2em",
                }}
              >
                Result: <span>{result}</span>
              </div>
            )}
          </div>
        )}
      </main>

      {/* Footer */}
      <footer
        style={{
          backgroundColor: "#333",
          textAlign: "center",
          padding: "15px",
          borderTop: "1px solid #ccc",
          color: "white",
        }}
      >
        &copy; Umesh, {currentYear}
      </footer>
    </div>
  );
}
