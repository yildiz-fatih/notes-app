import { useState, useEffect } from 'react'
import axios from 'axios'

const API = 'http://localhost:5001'

function App() {
  const [count, setCount] = useState(0)

  useEffect(() => {
    axios.get(`${API}/api/Counter`).then(res => setCount(res.data))
  }, [])

  const increment = () => {
    axios.post(`${API}/api/Counter`).then(res => setCount(res.data))
  }

  return (
    <div>
      <h1>Counter: {count}</h1>
      <button onClick={increment}>Increment</button>
    </div>
  )
}

export default App
