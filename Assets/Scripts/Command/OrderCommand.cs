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

    public OrderCommand(CustomerOrderController cnt, float makeOrderLimitTime, float getOrderLimitTime)
    {
        this.cnt = cnt;
        this.makeOrderLimitTime = makeOrderLimitTime;
        this.getOrderLimitTime = getOrderLimitTime;
    }

    public override async Task Execute()
    {
        counter = 0f;
        cnt.Randomize();
        cnt.ShowUI();

        while(cnt.IsClicked() == false)
        {
            if (token.IsCancellationRequested)
                throw new TaskCanceledException();

            counter += Time.deltaTime;
            if (counter > makeOrderLimitTime)
                break;
            await Task.Delay(100);
        }

        cnt.HideUI();
        if (!cnt.IsClicked())
        {
            return;
        }

        bool coffeMade = false;
        CoffeeMakingController.OnProperCoffePrepared += () => coffeMade = true;
        CoffeeMakingController.OnWrongCoffePrepared += () => coffeMade = true;
        
        counter = 0f;

        while (!coffeMade)
        {
            counter += Time.deltaTime;
            if (counter > makeOrderLimitTime)
                break;

            await Task.Delay(100);
        }
    }
}
