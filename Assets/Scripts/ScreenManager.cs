using System;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    private GameScreen currentScreen;

    public ScreenType initialScreen;

    public GameScreen foodStorageScreen;

    public GameScreen foodCounterScreen;

    public GameScreen foodPrepScreen;

    private GameScreen GetScreenFromEnum(ScreenType screenType)
    {
        switch (screenType)
        {
            case ScreenType.FoodStorage:
                return foodStorageScreen;
            case ScreenType.FoodCounter:
                return foodCounterScreen;
            case ScreenType.FoodPrep:
                return foodPrepScreen;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Awake()
    {
        Debug.Log("ScreenManager: Awake");
        foodStorageScreen.gameObject.SetActive(false);
        foodCounterScreen.gameObject.SetActive(false);
        foodPrepScreen.gameObject.SetActive(false);
    }

    private void Start()
    {
        Debug.Log("ScreenManager: Start");
        ChangeScreen(initialScreen);
    }

    public void ChangeScreen(ScreenType screenType)
    {
        Debug.Log("Changing to screen " + screenType);

        var screen = GetScreenFromEnum(screenType);

        if (screen == currentScreen) return;

        if (currentScreen != null)
            currentScreen.gameObject.SetActive(false);

        screen.gameObject.SetActive(true);
        currentScreen = screen;
    }

    public void ChangeToFoodStorage()
    {
        ChangeScreen(ScreenType.FoodStorage);
    }

    public void ChangeToFoodCounter()
    {
        ChangeScreen(ScreenType.FoodCounter);
    }

    public void ChangeToFoodPrep()
    {
        ChangeScreen(ScreenType.FoodPrep);
    }
}