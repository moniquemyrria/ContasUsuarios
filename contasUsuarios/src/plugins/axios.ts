var axios = require("axios");
import { Storage } from "../configuration/storage";

var axiosInstance = axios.create({

  //server teste kodigos
  // baseURL: 'http://10.0.0.6:5000/api/',
  
  //local
  baseURL: "http://localhost:5000/api/",//"http://localhost:5000/api/",
  
  //IP
  // baseURL: "http://mansrvapp:5020/api/",//"http://localhost:5000/api/",
  
  responseType: "json",
  responseEncoding: "utf8",
  headers: {
    Accept: "application/json",
    "Content-Type": "application/json;charset=UTF-8",
    "Access-Control-Allow-Origin": "*",
    "Access-Control-Allow-Methods": "GET,POST,PUT,DELETE,OPTIONS",
    PortalComprasSession: Storage.get("Session"),
  },
});

export default axiosInstance;
