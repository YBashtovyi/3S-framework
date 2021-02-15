<template>
    <div class="canvas-wrapper" v-if="chartData">
        <bar-chart 
            v-if="isBarChart" 
            :chart-data="chartData" />
        <line-chart 
            v-else-if="isLineChart"
            :chart-data="chartData" />
        <pie-chart 
            v-else-if="isPieChart"
            :chart-data="chartData" />
    </div>
</template>
<script>
import {
    BarChart,
    LineChart,
    PieChart
} from './mixins'
import { mapToChartData } from '../../utils/chart-helpers'
import isEmpty from 'lodash.isempty'
export default {
    components: {
        BarChart,
        LineChart,
        PieChart
    },
    props: {
        /**
         * Exepts certain type of chart. Following values are supported:
         * * `bar` - for creating chart with "columns"
         * * `line` - for creating line charts
         * * `pie` - for creating rounded chart with devided pieces with different colors
         */
        chartType: {
            type: String,
            required: true,
            validator: function (typeValue) {
                return ['bar', 'line', 'pie'].indexOf(typeValue) !== -1
            }
        },
        /**
         * data which is represented
         */
        dataCollection: {
            type: Array,
            required: true
        },
        /**
         * options which helps to convert certain `data` to `chartData`
         */
        dataOptions: {
            type: Object,
            required: true,
            default: function() {
                return {
                   labelOption: '',
                   valueOptions: []
                }
            }
        }
    },
    computed: {

        isLineChart() {
            return this.chartType === 'line'
        },

        isBarChart() {
            return this.chartType === 'bar'
        },

        isPieChart() {
            return this.chartType === 'pie'
        },

        chartData() {
            return !isEmpty(this.dataCollection) 
                ? mapToChartData(this.dataCollection, this.dataOptions) 
                : null
        }

    }
}
</script>
<style>
    .chartjs-render-monitor {
        position: relative; 
        height: 85vh !important;
    }
</style>