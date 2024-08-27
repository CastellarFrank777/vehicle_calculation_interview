import axios from 'axios'

const API_BASE_URL = import.meta.env.VITE_VEHICLE_API_BASE_URL

export interface Fees {
  basicFee: number
  specialFee: number
  associationFee: number
  storageFee: number
}

export interface CalculationResponse extends Fees {
  totalCost: number
}

export const calculateTotalCost = async (
  basePrice: number,
  vehicleType: string
): Promise<CalculationResponse> => {
  try {
    const response = await axios.post<CalculationResponse>(`${API_BASE_URL}/vehicle/calculate`, {
      basePrice,
      vehicleType
    })
    return response.data
  } catch (error) {
    console.error('Error calculating total cost:', error)
    throw error
  }
}
