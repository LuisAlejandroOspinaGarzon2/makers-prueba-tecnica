import axios from 'axios';

const API_URL = 'https://mocki.io/v1/7c033695-a247-4c07-a5ff-484598745462';

export const fetchGameData = async () => {
  try {
    const response = await axios.get(API_URL);
    return response.data;
  } catch (error) {
    console.error('Error fetching game data:', error);
    throw error;
  }
};
