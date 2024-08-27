<script lang="ts">
import { defineComponent, ref, watch } from 'vue'
import { calculateTotalCost, Fees } from '../../../services/apiService'
import FeeDisplay from '../FeeDisplay/FeeDisplay.vue'

export default defineComponent({
  name: 'CalculationForm',
  components: {
    FeeDisplay
  },
  setup() {
    const vehiclePrice = ref<number>(0)
    const vehicleType = ref<string>('Common')
    const fees = ref<Fees>({
      basicFee: 0,
      specialFee: 0,
      associationFee: 0,
      storageFee: 0
    })
    const totalCost = ref<number>(0)
    const showResults = ref<boolean>(false)
    const debounceDelay = ref<number>(parseInt(import.meta.env.VITE_FORM_DEBOUNCE_DELAY) || 500)
    let debounceTimer: ReturnType<typeof setTimeout>

    const fetchCalculation = async () => {
      try {
        const data = await calculateTotalCost(vehiclePrice.value, vehicleType.value)
        fees.value = {
          basicFee: data.basicFee,
          specialFee: data.specialFee,
          associationFee: data.associationFee,
          storageFee: data.storageFee
        }
        totalCost.value = data.totalCost
        showResults.value = true
      } catch (error) {
        console.error('Error calculating total:', error)
      }
    }

    // Watch for changes in vehiclePrice or vehicleType and recalculate
    watch([vehiclePrice, vehicleType], () => {
      // Validate input before setting timeout
      if (vehiclePrice.value <= 0) {
        showResults.value = false
        return
      }
      clearTimeout(debounceTimer)
      debounceTimer = setTimeout(() => {
        fetchCalculation()
      }, debounceDelay.value)
    })

    return {
      vehiclePrice,
      vehicleType,
      fees,
      totalCost,
      showResults
    }
  }
})
</script>

<template>
  <div class="form-container">
    <form class="calculation-form">
      <div class="form-group">
        <label for="vehiclePrice">Vehicle Price</label>
        <input
          id="vehiclePrice"
          type="number"
          v-model.number="vehiclePrice"
          placeholder="Enter vehicle price"
        />
      </div>
      <div class="form-group">
        <label for="vehicleType">Vehicle Type</label>
        <select id="vehicleType" v-model="vehicleType">
          <option value="Common">Common</option>
          <option value="Luxury">Luxury</option>
        </select>
      </div>
      <div v-if="showResults" class="form-group">
        <FeeDisplay label="Basic Fee" :value="fees.basicFee" />
        <FeeDisplay label="Special Fee" :value="fees.specialFee" />
        <FeeDisplay label="Association Fee" :value="fees.associationFee" />
        <FeeDisplay label="Storage Fee" :value="fees.storageFee" />
        <h4>Total Cost: {{ totalCost.toFixed(2) }}</h4>
      </div>
    </form>
  </div>
</template>

<style scoped>
@import './CalculationForm.css';
</style>
