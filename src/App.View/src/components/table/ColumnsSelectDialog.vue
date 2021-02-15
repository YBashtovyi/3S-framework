<template>
  <q-dialog
    persistent
    v-model="isDialogVisible"
  >
    <q-card style="width: 400px; max-width: 80vw;">
      <q-card-section class="text-subtitle2 text-uppercase q-pa-md bg-primary text-white">
        Вибір колонок
      </q-card-section>
      <q-form @submit="onCardSubmit()">
        <q-card-section class="q-pa-sm">
          <div
            v-for="(col,i) in columns"
            :key="col.name"
          >
            <q-checkbox
              v-model="selection[i]"
              :label="col.label"
              v-if="!columnsSelectListSkip.includes(col.name)"
            ></q-checkbox>
          </div>
        </q-card-section>
        <q-card-actions
          align="right"
          class="q-pa-sm"
        >
          <q-checkbox
            v-model="isAllChecked"
            :label="isAllChecked ? `Зняти усі позначки` : `Обрати всі`"
            @input="onAllClicked"
            class="q-mr-auto"
          ></q-checkbox>
          <q-btn
            label="Відмінити"
            color="negative"
            flat
            @click="onCardCancel"
          />
          <q-btn
            type="submit"
            label="Завантажити"
            color="primary"
            class="on-right"
            :disable="isValid"
            @click="onCardSubmit"
          />
        </q-card-actions>
      </q-form>
    </q-card>
  </q-dialog>
</template>

<script>
import axios from '@/utils/axios-mis'
export default {
  name: 'ColumnSelectedDialog',
  data () {
    return {
      toggleIndeterminate: false,
      clientColumns: [],
      selection: [],
      isAllChecked: true,
      isValid: false
    }
  },
  props: {
    columns: Array,
    // Define which columns need to be excluded in column select dialog
    columnsSelectListSkip: Array,
    isDialogVisible: Boolean,
    searchDictionary: Array,
    selectedfilter: {},
    sort: String
  },
  methods: {
    onCardSubmit (event) {
      this.clientColumns = []
      if (event) event.preventDefault()
      for (var i = 0; i < this.selection.length; i++) {
        if (this.selection[i] === true) {
          var item = this.columns[i]
          if(!this.columnsSelectListSkip.includes(item.name)){
            this.clientColumns.push(item)
          }
        }
      }
      this.getExcelDocument(this.clientColumns)
    },
    getExcelDocument (clientColumns) {
      
      this.isColumnSelectDialogVisible = false
      var cols = {}

      for (var i = 0; i < clientColumns.length; i++) {
        cols[clientColumns[i].name] = clientColumns[i].label
      }

      var date = new Date()
      var timeZoneOffsetMinutes = -date.getTimezoneOffset()

      var paramList = {}
      for (var j = 0; j < this.searchDictionary.length; j++) {
        paramList[this.searchDictionary[j].key] = this.searchDictionary[j].value
      }

      var orderBy = this.sort
      var data = {
        'paramList': paramList,
        'orderBy': orderBy,
        'columns': cols,
        'timeZoneOffsetMinutes': timeZoneOffsetMinutes
      }
      
      axios.post(this.selectedfilter.value, data, { headers: { 'Accept': 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' }, responseType: 'arraybuffer' })
        .then(response => {
          if (response) {
            this.$emit('dialogCloseEvent')
          }
          let url = window.URL.createObjectURL(new Blob([response.data], { type: 'application/vnd.ms-excel' }))
          let link = document.createElement('a')
          link.href = url
          link.type = 'arraybuffer'
          link.setAttribute('download', 'List.xlsx')
          document.body.appendChild(link)
          this.onCardCancel()
          link.click()
          setTimeout(() => {
            document.body.removeChild(link)
          }, 1000)
        })
    },
    onCardCancel () {
      this.$emit('dialogCloseEvent')
    },
    onAllClicked () {
      var val = this.selection[0]
      this.selection = []
      for (var i = 0; i < this.columns.length; i++) {
        if (this.isAllChecked !== !val) {
          this.selection.push(val)
          this.isAllChecked = val
        } else {
          this.selection.push(!val)
          this.isAllChecked = !val
        }
      }
    }
  },
  watch: {
    selection: function () {
      let isEmpty = true
      this.isAllChecked = true
      this.selection.forEach(element => {
        if (element === true) {
          isEmpty = false
        } else {
          this.isAllChecked = false
        }
      })
      this.isValid = isEmpty
    }
  },
  created () {
    for (var i = 0; i < this.columns.length; i++) {
      this.selection.push(true)
    }
  }
}
</script>
