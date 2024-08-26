import React, { useState, useEffect } from 'react';
import { fetchGameData } from '../Services/apiService';

const GameForm = () => {
  const [gameData, setGameData] = useState(null);
  const [fetchError, setFetchError] = useState(null);

  useEffect(() => {
    const retrieveGameData = async () => {
      try {
        const data = await fetchGameData();
        setGameData(data);
      } catch (err) {
        setFetchError('Game data not found :(');
      }
    };
    retrieveGameData();
  }, []);

  return (
    <div>
      {fetchError && (
        <>
          <div class="p-4 text-sm text-red-800 rounded-lg bg-red-50 dark:bg-gray-800 dark:text-red-400" role="alert">
            <span class="font-medium">Something went wrong! </span>{fetchError}
          </div>
        </>
      )}
      {gameData && (
        <>
          <img
            src={gameData.background_image}
            alt={gameData.name}
            className="w-full h-auto object-cover rounded-t-lg"
          />
          <div className="p-6">
            <h2 className="text-2xl font-bold text-gray-800 mb-4">{gameData.name}</h2>
            <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
              <div>
                <label className="block text-gray-700 text-sm font-bold mb-2">Release Date:</label>
                <div className="text-gray-800">{gameData.released}</div>
              </div>
              <div>
                <label className="block text-gray-700 text-sm font-bold mb-2">Rating:</label>
                <div className="text-gray-800">{gameData.rating}</div>
              </div>
              <div>
                <label className="block text-gray-700 text-sm font-bold mb-2">Genres:</label>
                <ul className="list-disc pl-5 text-gray-800">
                  {gameData.genres.map((genre) => (
                    <li key={genre.id}>{genre.name}</li>
                  ))}
                </ul>
              </div>
              <div>
                <label className="block text-gray-700 text-sm font-bold mb-2">Platforms:</label>
                <ul className="list-disc pl-5 text-gray-800">
                  {gameData.platforms.map((platform) => (
                    <li key={platform.id}>{platform.name}</li>
                  ))}
                </ul>
              </div>
            </div>
          </div>
        </>
      )}
    </div>
  );
};

export default GameForm;
