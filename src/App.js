import React from 'react';
import GameForm from './Components/GameForm';

const App = () => {
  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-900">
      <div className="max-w-lg w-full bg-slate-200 rounded-lg shadow-lg overflow-hidden">
        <GameForm />
      </div>
    </div>
  );
}

export default App;
