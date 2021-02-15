import { fetchConstructionObjectExProperty } from '../../../services/classifiers/constuction-object-ex-property-dict'
import get from 'lodash.get'
import set from 'lodash.set'
import { stringEmpty } from '../../../utils/function-helpers'

export default {
  data() {
    return {
      typeOfObjectProperties: [],
      selectedTypeOfObjectProperty: null,

      typeOfObjectSubProperties: [],
      selectedTypeOfObjectSubProperty: null,

      description: '',
    }
  },

  watch: {
    isVisibleDialog(value) {
      if (value) {
        this.selectedTypeOfObjectProperty = null
        this.selectedTypeOfObjectSubProperty = null
        this.typeOfObjectProperties = []
        this.typeOfObjectSubProperties = []
        this.description = stringEmpty()

        this.getTypeOfObjectProperties(this.typeOfObjectId).then(this.setTypeOfObjectProperty)
      }
    },
  },

  props: {
    isVisibleDialog: {
      type: Boolean,
      default: false,
    },

    typeOfObjectId: {
      type: String,
      required: true,
    },

    savedExProperties: {
      type: Array,
      required: true,
    },
  },

  methods: {
    getTypeOfObjectProperties(parentId) {
      const param = {
        parentId: parentId,
      }
      return fetchConstructionObjectExProperty(param)
    },

    setTypeOfObjectProperty(data) {
      this.typeOfObjectProperties = data.filter(p => {
        return !this.savedExProperties.find(z => z.constructionObjectExPropertyCode === p.code)
      })
    },

    onSubmit() {
      this.$emit('update:isVisibleDialog', false)
      const extendedProperty = {
        dictionaryId: this.selectedTypeOfObjectSubProperty.id,
        constructionObjectExPropertyId: this.selectedTypeOfObjectProperty.id,
        constructionObjectSubExPropertyId: this.selectedTypeOfObjectSubProperty.id,
        value: this.selectedTypeOfObjectSubProperty.code,
        valueName: this.selectedTypeOfObjectSubProperty.name,
        description: this.description,
      }
      this.$emit('addExtendedProperty', extendedProperty)
    },

    onPropertyChanged(property) {
      set(this, ['selectedTypeOfObjectProperty'], property)
      const propertyId = get(property, ['id'], stringEmpty())
      const propertyDataFormat = get(property, ['dataFormat'], stringEmpty())

      this.typeOfObjectSubProperties = []
      this.selectedTypeOfObjectSubProperty = null
      // TODO: add constant
      if (propertyDataFormat === 'EnumRecord') {
        this.getTypeOfObjectProperties(propertyId).then(this.setTypeOfObjectSubProperty)
      }
    },

    setTypeOfObjectSubProperty(data) {
      this.typeOfObjectSubProperties = data
    },
  },
}
