import axios from "axios";

const base_url = "https://localhost:5001/api/traceroute";

const getTraceRoute = async (hostname) => {
  const result = await axios.get(`${base_url}/`);
  return result.data;
};

const postTraceRoute = async (hostname, numIterations) => {
  const obj = {
    hostname: hostname,
    numIterations: numIterations,
  };
  const result = await axios.post(`${base_url}`);
  return result.data;
};

export default { getTraceRoute, postTraceRoute };
