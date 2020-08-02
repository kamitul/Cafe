using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class OrderCommand : Command
{
    private CustomerOrderController cnt;
    private float takeOrderLimitTime;
    private float getOrderLimitTime;
    private float counter;
    private float tick;

    private bool coffeMade = false;
    private bool isCorrect;

    public OrderCommand(CustomerOrderController cnt, float takeOrderLimitTime, float getOrderLimitTime)
    {
        this.cnt = cnt;
        this.takeOrderLimitTime = takeOrderLimitTime;
        this.getOrderLimitTime = getOrderLimitTime;
        this.tick = 0f;
    }

    public override async Task Execute()
    {
        await TakeOrder();
        if (!cnt.IsClicked())
        {
            cnt.RecieveOrder(false);
            return;
        }

        CoffeeMakingController.OnProperCoffePrepared += (Order order) => { isCorrect = true; CheckForCoffe(order); };
        CoffeeMakingController.OnWrongCoffePrepared += (Order order) => { isCorrect = false; CheckForCoffe(order); };

        await GetOrder();

        if (!coffeMade)
        {
            cnt.RecieveOrder(false);
            return;
        }

        cnt.ToggleDoneOrder(true);
        await Task.Delay(100);
        cnt.ToggleDoneOrder(false);
    }

    private async Task GetOrder()
    {
        counter = 0f;
        cnt.ToggleWaitForOrder(true);
        coffeMade = false;
        tick = Time.deltaTime;

        while (!coffeMade)
        {
            counter += Math.Abs(tick);
            tick = Time.time;
            if (counter > getOrderLimitTime)
            { cnt.BreakOrder(); break; }
            await Task.Delay(100);
            tick -= Time.time;
        }

        cnt.ToggleWaitForOrder(false);
    }

    private async Task TakeOrder()
    {
        counter = 0f;
        cnt.RandomizeCoffee();
        cnt.ToggleSetOrder(true);
        tick = Time.deltaTime;

        while (!cnt.IsClicked())
        {
            if (token.IsCancellationRequested)
                throw new TaskCanceledException();

            counter += Math.Abs(tick);
            tick = Time.time;
            if (counter > takeOrderLimitTime)
            { break; }
            await Task.Delay(100);
            tick -= Time.time;
        }
        cnt.ToggleSetOrder(false);
    }

    private void CheckForCoffe(Order obj)
    {
        if (obj.OrderIdentfier == cnt.OrderInfo.OrderIdentfier)
        {
            coffeMade = true;
            cnt.RecieveOrder(isCorrect);
        }
    }
}
