import axios from "axios";

const base_url = "https://localhost:5001/api/traceroute";

const getTraceRoutes = async () => {
  const result = await axios.get(`${base_url}/`);
  return result.data;
};
const getTraceRouteCollection = async (collectionID) => {
  const result = await axios.get(`${base_url}/${collectionID}`);
  return result.data;
};

const postTraceRoute = async (obj) => {
  try {
    const result = await axios.post(`${base_url}`, obj);
    return result;
  } catch (err) {
    return err.response;
  }
};

export default {
  getTraceRoutes,
  postTraceRoute,
  getTraceRouteCollection,
};
