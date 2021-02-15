<template>
	<div class="row bg-grey-2 q-pl-md q-py-xs no-wrap justify-between items-center">
		
		<!-- header general info section -->
		<q-field borderless class="full-width">
			<template v-slot:before>
				<q-icon :name="icon" color="primary" :class="iconClass" />
			</template>
			<template v-slot:control>				
          		<div class="text-primary self-center full-width no-outline" tabindex="0">{{ label }}</div>
				<div v-if="!labelLink">{{ content }}</div>
				<router-link
					v-else
					:to="labelLink"
					class="text-primary"
					title="Перейти на картку пацієнта"
				>{{ content }}</router-link>				
          		<div class="self-center full-width no-outline" tabindex="0">{{ contentExt }}</div>
			</template>
		</q-field>
		
		<q-space />

		<!-- header notify message section -->
		<div v-if="notifyText" style="width: 100%" class="row justify-end items-center content-center">
			<div
				:class="$q.screen.gt.sm ? ' notify-card q-ma-none' : 'fixed-bottom column items-center justify-center notify-card z-top'"
			>
				<div :class="successMessage ? 'notify-card-inner' : 'text-grey-8 bg-amber-3 notify-card-inner'">
					<q-icon :name="iconName" class="on-left" size="22px" />
					<div class="caption on-left">{{ notifyText}}</div>
				</div>
			</div>
		</div>

		<!--header title custom buttons -->
		<div v-for="(item, index) in headerButtons"  
			:key="index">
			<q-btn v-if="item.visible"
				:disable="item.disable"
				flat
				:icon="item.icon"
				:color="item.color ? item.color: 'primary'"
				no-wrap
				:label="item.label"
				:size="item.size? item.size: `12px`"
				@click="item.action"
				class="q-mr-sm"
			>
			<q-tooltip v-if="item.tooltipLabel" anchor="center left"
					self="center right"
					:offset="[10, 10]"
					content-style="font-size: 13px">{{ item.tooltipLabel }}</q-tooltip>
			</q-btn>
		</div>

		<!-- header action button -->
		<q-fab
			v-if="headerActions.length > 0"
			flat
			round
			unelevated
			dense
			icon="more_vert"
			direction="down"
			class="q-mr-sm"
			color="grey-3"
			text-color="primary"
		>
			<q-btn
				round
				color="grey-4"
				text-color="grey-8"
				:to="item.link"
				@click="item.action"
				size="md"
				v-for="(item, index) in headerActions"
				v-entity-right="item.entityRight"
				v-operation-right="item.operationRight"
				:key="index"
				:disable="item.disable"
			>
				<q-icon center :size="item.iconSize" :name="item.icon" />
				<q-tooltip
					anchor="center left"
					self="center right"
					:offset="[10, 10]"
					content-style="font-size: 13px"
				>{{ item.label }}</q-tooltip>
			</q-btn>
		</q-fab>

		<q-btn v-if="link" color="info" flat round :to="link" class="on-left">
			<q-icon center size="18px" name="far fa-edit" />
		</q-btn>
	</div>
</template>

<script>
import { mapState } from "vuex"
export default {
	props: {
		label: String,
		labelLink: {
			type: String,
			default: ""
		},
		content: String,
		contentExt: {
			type: String,
			default: ""
		},
		icon: String,
		iconClass: String,
		link: {
			type: String,
			default: ""
		},
		successMessage: {
			type: Boolean,
			default: false
		},
		notifyText: {
			type: String,
			default: ""
		},
		headerButtons: {
			type: Array,
			default: function() {
				return []
			}
		},
		actions: {
			type: Array,
			default: function() {
				return []
			}
		}
	},

	computed: {
		...mapState("baseElements", ["rights"]),

		// actions to be displayed
		// this array is filled with actions prop
		headerActions() {
			// copy actions props into another array
			// and set action to empty function if not set, because @click event does not support undefined
			let counter = 0
			const headerActions = []
			this.actions.forEach(element => {
				headerActions.push(element)
				if (!element.action) {
					headerActions[counter].action = () => {}
				}
				counter++
			})

			return headerActions
		},

		iconName() {
			return this.successMessage ? "done_all" : "warning"
		}
	}
}
</script>
