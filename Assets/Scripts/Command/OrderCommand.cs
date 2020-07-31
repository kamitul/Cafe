using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class OrderCommand : Command
{
    private CustomerOrderController cnt;
    private float makeOrderLimitTime;
    private float getOrderLimitTime;
    private float counter;

    private bool coffeMade = false;
    private bool isCorrect;

    public OrderCommand(CustomerOrderController cnt, float makeOrderLimitTime, float getOrderLimitTime)
    {
        this.cnt = cnt;
        this.makeOrderLimitTime = makeOrderLimitTime;
        this.getOrderLimitTime = getOrderLimitTime;
    }

    public override async Task Execute()
    {
        counter = 0f;
        cnt.RandomizeCoffee();
        cnt.ToggleSetOrder(true);

        while(cnt.IsClicked() == false)
        {
            if (token.IsCancellationRequested)
                throw new TaskCanceledException();

            counter += Time.deltaTime;
            if (counter > makeOrderLimitTime)
            { cnt.BreakOrder(); break; }
            await Task.Delay(100);
        }

        cnt.ToggleSetOrder(false);
        if (!cnt.IsClicked())
        {
            cnt.RecieveOrder(false);
            return;
        }

        cnt.ToggleWaitForOrder(true);
        coffeMade = false;
        CoffeeMakingController.OnProperCoffePrepared += (Order order) => { isCorrect = true; CheckForCoffe(order); };
        CoffeeMakingController.OnWrongCoffePrepared += (Order order) => { isCorrect = false; CheckForCoffe(order); };
        counter = 0f;

        while (!coffeMade)
        {
            counter += Time.deltaTime;
            if (counter > makeOrderLimitTime)
            { cnt.BreakOrder(); break; }
            await Task.Delay(100);
        }

        cnt.ToggleWaitForOrder(false);

        if (!coffeMade)
        {
            cnt.RecieveOrder(false);
            return;
        }

        cnt.ToggleDoneOrder(true);
        await Task.Delay(100);
        cnt.ToggleDoneOrder(false);
    }

    private void CheckForCoffe(Order obj)
    {
        if(obj.OrderIdentfier == cnt.CustomerGUID)
        {
            coffeMade = true;
            cnt.RecieveOrder(isCorrect);
        }
    }
}
