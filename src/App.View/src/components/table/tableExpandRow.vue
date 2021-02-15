<template>
  <div v-if="visible">
    <div
      v-for="(item, index) in expanded"
      :key="index"
      class="text-caption row"
    >
      <span style="font-weight: 600; width: 200px">{{ item.label }}:</span>
      <span class="col-8">{{ item.caption }}</span>
    </div>
  </div>
</template>

<script>
import { date } from "quasar";
export default {
  props: {
    expanded: {
      type: [Boolean, Array],
      default: false
    },
    row: Object,
    visible: Boolean
  },
  methods: {
    onCreated() {
      if (this.visible) {
        this.expanded.forEach(expandedColumn => this.setRowValue(expandedColumn))
      }
    },
    setRowValue(expandedColumn) {
      if (expandedColumn.field) {
        expandedColumn.caption = this.formatCaption(expandedColumn.field(this.row), expandedColumn.type);
      }
      else
      {
        for (let key in this.row) {
          this.evaluateCaption(key, expandedColumn)
        }
      }
    },
    evaluateCaption(rowKey, expandedColumn) {
      if (rowKey === expandedColumn.name) {
        expandedColumn.caption = this.formatCaption(this.row[rowKey], expandedColumn.type);
      }
    },
    formatCaption(caption, type)
    {
      switch(type) {
        case "date": {
          return date.formatDate(caption, "DD.MM.YYYY");
        }
        case "time": {
          return date.formatDate(caption, "HH:mm");
        }
        case "date-time": {
          return date.formatDate(caption, "DD.MM.YYYY HH:mm");
        }
        default: {
          return caption;
        }
      }
    }
   },
  created() {
    this.onCreated();
  }
};
</script>
