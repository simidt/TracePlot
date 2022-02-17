import axios from "axios";

// TODO: Find a better way to do this
const base_url = `${window.location.origin}/api/traceroute`;

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

const traceRouteService = {
  getTraceRoutes,
  postTraceRoute,
  getTraceRouteCollection,
};
export default traceRouteService;
