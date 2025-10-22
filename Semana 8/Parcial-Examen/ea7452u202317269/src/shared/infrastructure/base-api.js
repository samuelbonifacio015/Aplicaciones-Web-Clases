import axios from "axios";

const aldiApi = import.meta.env.VITE_ALDI_API_URL;

/**
 * Base API class to handle HTTP requests using Axios
 * @class
 * @author Samuel Bonifacio - u202317269
 */
export class BaseApi {
    /**
     * @private
     * Axios HTTP client instance
     * @type {import('axios').AxiosInstance}
     */
    #http;

    /**
     * Initializes the Axios HTTP client with the base URL from environment variables
     */
    constructor() {
        this.#http = axios.create({
            baseURL: aldiApi
        });
    }

    /**
     * Gets the Axios HTTP client instance
     * @returns {axios.AxiosInstance}
     */
    get http() {
        return this.#http;
    }

}