export default {
    data () {
        return {
            emptyGuid: '00000000-0000-0000-0000-000000000000'
        }
    },
    methods: {
        
        /**
         * Return new Guid
         */
        getNewGuid () {
            return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
                (c ^(crypto.getRandomValues(new Uint8Array(1))[0] & (15 >> (c / 4)))).toString(16))
        },
        
        /**
         * Return true if guid = empty
         * @param {*} value  - guid value
         */
        isEmptyGuid (value) {
            return value === this.emptyGuid
        }
    }
}