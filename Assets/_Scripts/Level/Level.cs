using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Car CarPrefab;
    public FinishLine FinishLinePrefab;
    public GameObject Platform;



    [SerializeField] Car[] _cars;
    [SerializeField] FinishLine[] _finishLine;

    private List<GameObject> CarsStillInGame;
    private int _carStillInGameAmount;

    private void Start()
    {
        CarsStillInGame = new List<GameObject>();
        foreach (Car c in _cars)
        {
            CarsStillInGame.Add(c.gameObject);
        }
        _carStillInGameAmount = CarsStillInGame.Count;
    }

    

    public void CarEnteredFinishLine(Car car)
    {  
        StopTheCar(car);
        CarsStillInGame.Remove(car.gameObject);
        _carStillInGameAmount--;
        if (_carStillInGameAmount <= 0) LevelWin();
    }

    public void StartLevel()
    {
        StartAllCars();
    }

    void StartAllCars()
    {
        foreach (Car c in _cars)
        {
            c.StartCar();
        }
    }

    void LevelWin()
    {
        Invoke("NextLevel",1f);
    }

    void NextLevel()
    {
        LevelManager.Instance.NextLevel();

    }

    void LevelLost()
    {
        Debug.Log("LEVEL LOST");
    }


    void StopTheCar(Car car)
    {
        car.StopCar();
    }

    public void AddCarToTheLevel(Car car)
    {
        if(_cars.Length == 0)
        {
            _cars = new Car[1];
            _cars[0] = car;
            return;
        }

        Car[] temp = _cars;
        _cars = new Car[_cars.Length+1];
        for(int i = 0; i < _cars.Length-1; i++)
        {
            _cars[i] = temp[i];
        }
        _cars[_cars.Length - 1] = car;
    }
}
