import { Line, mixins } from 'vue-chartjs'
const { reactiveProp } = mixins

export default {
    extends: Line,
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