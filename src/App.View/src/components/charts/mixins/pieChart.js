import { Pie, mixins } from 'vue-chartjs'
const { reactiveProp } = mixins

export default {
    extends: Pie,
    data() {
        return {
          options: {
            responsive: true,
            maintainAspectRatio: false
          }
        }
      },
      mixins: [reactiveProp],
      mounted() {
          this.renderChart(this.chartData, this.options)
      }
}