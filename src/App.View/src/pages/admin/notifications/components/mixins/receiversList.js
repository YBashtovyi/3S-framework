export default {

    props: {

        receivers: {
            
            type: Array,
            default: function() {
                return []
            }
        }
    },


    data() {

        return {

            addReceiverVisible: false,
            selectedUsers: [],
            employeesIds: []
        }
    },

    methods: {

        onAddReceiver() {

            this.employeesIds = this.receivers.map((x) => {
                return x.receiverId
            })

            this.showReceiverDialog()
        },


        showReceiverDialog() {

            this.addReceiverVisible = true
        },


        hideReceiverDialog() {

            this.addReceiverVisible = false
        },


        onDeleteReceiver(item) {

            this.$emit('remove', item)
        },


        onCloseReceiverDialog() {

            this.hideReceiverDialog()
        },


        onSuccessReceiverDialog(employees) {

            this.hideReceiverDialog()
            this.$emit('success', employees)
        }
    }
}