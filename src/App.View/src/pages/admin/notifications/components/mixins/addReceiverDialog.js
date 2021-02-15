import {
    fetchEmployeesWithUserId
} from '../../../../../services/apiServices/employee-api'
import isEmpty from 'lodash.isempty'
import get from 'lodash.get'
import isEqual from 'lodash.isequal'

export default {
    
    props: {

        selectedEmployeeIds: {
            
            type: Array,
            default: function() {
                return []
            }
        }
    },

    data() {

        return {
            
            mainFilter: '',

            dialogVisible: false,
            dataLoaded: false,
            hasErrors: false,

            employees: [],
            filteredEmployees: [],
            
            selectedEmployees: [],
            columns: [
                {
                    name: 'caption',
                    required: true,
                    label: 'ПІБ співробітника',
                    align: 'left',
                    sortable: true,
                    field: "caption"
                },
                {
                    name: 'postionTypeCaption',
                    required: true,
                    label: 'Посада',
                    align: 'left',
                    sortable: true,
                    field: "postionTypeCaption"
                },
                {
                    name: 'subscribedToNotifications',
                    required: true,
                    label: 'Підписаний на сповіщення',
                    align: 'left',
                    isCheckBox: true,
                    sortable: true,
                    field: "subscribedToNotifications"
                }
            ],

            checkBoxAll: false
        }
    },

    
    watch: {

        checkBoxAll(value) {

            if (value) {

                
                this.selectedEmployees = this.filteredEmployees
                return
            }

            this.selectedEmployees = []
        },


        selectedEmployees() {
            
            if (!isEmpty(this.selectedEmployees)) {

                if (isEqual(this.selectedEmployees.length, this.filteredEmployees.length)) {

                    this.checkBoxAll = true
                    return
                }
            }

            this.checkBoxAll = false
        }
    
    },
    
    created() {

        this.showDialog()
        this.getPageData()
    },
    

    methods: {

        getPageData() {

            fetchEmployeesWithUserId()
                .then(this.setEntityData)
                .then(this.setDataLoadStatus)
                .catch(this.setHasErrorsStatus)
        },


        setEntityData(data) {

            this.employees = data.filter(x => !isEmpty(x.userId))
            this.filteredEmployees = this.employees
            this.selectedEmployees = data.filter(x => this.selectedEmployeeIds.includes(x.id))
        },

        onFIOChanged() {

            if (isEmpty(this.mainFilter)) {

                this.filteredEmployees = this.employees
            }
            else {
                
                this.filteredEmployees = this.employees.filter(x => x.caption.toLowerCase().includes(this.mainFilter.toLowerCase()))
            }
        },


        setDataLoadStatus() {

            this.dataLoaded = true
        },


        setHasErrorsStatus() {
            
            this.hasErrors = true
        },


        onSubmit() {

            if (isEmpty(this.selectedEmployees)) {

                this.notifyIfWarning('Потрібно обрати хоча б одного отримувача')
                return
            }

            const selectedEmployees = []

            for (let index = 0; index < this.selectedEmployees.length; index++) {

                const element = this.selectedEmployees[index]
                const selectedEmployee = this.employees.find(x => x.id === element.id)

                const newElement = {
                    receiverId: get(selectedEmployee, 'id', null),
                    receiverCaption: get(selectedEmployee, 'caption', '')
                }

                selectedEmployees.push(newElement)
            }            
            
            this.hideDialog()
            this.$emit('success', selectedEmployees)
        },


        onCancel() {

            this.closeDialog()
        },

        showDialog() {

            this.dialogVisible = true
        },

        hideDialog() {

            this.dialogVisible = false
        },

        closeDialog() {

            this.hideDialog()
            this.$emit("close")
        },
        
        /**
         * notify user if something warning
         * @param {*} message 
         */
        notifyIfWarning(message) {

            this.$q.notify({
                timeout: 5000,
                message: message,
                color: 'warning',
                icon: 'warning',
                textColor: 'grey-9'
            })
        }
    }
}