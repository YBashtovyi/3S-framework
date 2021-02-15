import { getData } from "@/services/api";

export default {
  namespaced: true,
  state: () => ({
    status: "initial",
    visibleSearch: false,
    search: "",
    sort: "",
    searchDictionary: [],
    tableData: [],
    paginationTable: null,
    fileFormats: [],
    ignoredColumns: []
  }),
  actions: {
    // clear tabledata
    clearTable({ commit }) {
      let data = [];
      commit("setTableData", {
        data
      });
    },

    // action to build table
    init({ commit, dispatch }, { pagination, params, requestUrl, searchDictionary }) {
      // call request action
      dispatch("initRequest", { pagination, params, requestUrl, searchDictionary})
        .then(tableData => {
          // save table data
          commit("setTableData", { tableData });
          // status for loader
          commit("setStatusDone");
        })
        .catch(error => {
          console.log(error);
          // status for loader
          commit("setStatusFailed");
        });
    },

    // init request for get table data
    initRequest( { commit, dispatch }, { pagination, params, requestUrl, searchDictionary }) {
      
      // set pagination
      let value = Object.assign({}, pagination);
      commit("setPagination", { value });
      dispatch("clearTable");

      let sort = null, url = "";

      if (pagination) {
        // set sort params for request ????
        if (pagination.descending === true) {
          sort = "-" + pagination.sortBy;
        } else {
          sort = pagination.sortBy;
        }

        commit("setSearch", { params });
        
        // check url&search params
        if (params) {
          url = `${requestUrl}${params}&pageNumber=${pagination.page}&pageSize=${pagination.rowsPerPage}&orderBy=${sort}`;
        } else if (requestUrl.indexOf("?") > 0) {
          url = `${requestUrl}&pageNumber=${pagination.page}&pageSize=${pagination.rowsPerPage}&orderBy=${sort}`;
        } else {
          url = `${requestUrl}?pageNumber=${pagination.page}&pageSize=${pagination.rowsPerPage}&orderBy=${sort}`;
        }
      } else {
        url = requestUrl;
      }

      commit("setSort", sort);

      if (searchDictionary) {
        commit("setSearchDictionary", searchDictionary);
      }

      // status for loader
      commit("setStatusLoading");

      if (!requestUrl) {
        return []
      }

      // return response data
      return new Promise((resolve, reject) =>
        getData(url)
          .then(response => {
            if (pagination && response.data.length > 0) {
              // save row number from response
              pagination.rowsNumber = response.data[0].totalRecordCount;

              // update pagination in store
              let value = Object.assign({}, pagination);
              commit("setPagination", { value });
            }
            resolve(response.data);
          })
          .catch(error => {
            reject(error);
          })
      );
    },

    // toggle search form visibility
    toggleSearch({ commit }) {
      commit("setVisibilitySearch");
    },
    clearSearchParams({ commit, params }) {
      commit("setSearch", {
        params
      });
    },
    setSearchParams({ commit }, { params }) {
      commit("setSearch", {
        params
      });
    },
    setSearchDictionaryValues({ commit, params }) {
      if (params) {
        commit("setSearchDictionary", params);
      }
    },
    setfileFormats({ commit, params }) {
      if (params) {
        commit("setfileFormats", params);
      }
    }
  },
  mutations: {
    setTableData(state, { tableData }) {
      state.tableData = tableData;
    },
    setPagination(state, { value }) {
      state.paginationTable = value;
    },
    setSearch(state, { params }) {
      state.search = params;
    },
    setSearchDictionary(state, params) {
      state.searchDictionary = params;
    },
    setSort(state, params) {
      state.sort = params;
    },
    setfileFormats(state, params) {
      state.fileFormats = params;
    },
    setIgnoredColumns(state, params) {
      state.ignoredColumns = params;
    },
    setStatusLoading(state) {
      state.status = "loading";
    },
    setStatusDone(state) {
      state.status = "done";
    },
    setStatusFailed(state) {
      state.status = "failed";
    },
    setVisibilitySearch(state) {
      state.visibleSearch = !state.visibleSearch;
    }
  }
};
