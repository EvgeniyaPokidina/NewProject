using System.IO;
Officiant officiant = new Officiant();
Cooker cooker = new Cooker();
Deployment deployment = new Deployment();
Cashier cashier = new Cashier();

Controller mediator = new Controller(officiant, cooker, deployment, cashier);

officiant.ExucuteWork("Заказ на пиццу");
cooker.GiveCommand("Пицца");


cooker.GiveCommand(""); 


interface IMediator
{
    void Notify(Employee employee, string msg);
}
abstract class Employee
{
    protected IMediator Mediator;
    public Employee(IMediator mediator) => Mediator = mediator;
    public void SetMediator(IMediator mediator) => Mediator = mediator;
}
class Officiant : Employee
{
    public Officiant(IMediator mediator = null!) : base(mediator) { }
    public void ExucuteWork(string work)
    {
        Console.WriteLine("Официант передал кассиру:" + work);
        Mediator.Notify(this, work);
    }
    public void SetWork(bool state)
    {
        if (state) Console.WriteLine("Официант занят");
        else Console.WriteLine("Официант свободен");
    }
}
class Cooker : Employee
{
    private string text;
    public Cooker(IMediator mediator = null!) : base(mediator) { }
    public void GiveCommand(string _text)
    {
        text = _text;
        if (text == "") Console.WriteLine("Заказа нет");
        else Console.WriteLine("Нужно заказать:" + text);
        Mediator.Notify(this, text);
    }
}

class Deployment : Employee
{
  
    public Deployment(IMediator mediator=null!) : base(mediator)
    {

    }
    public void Explorer(string _text)
    {
        if (_text == "") Console.WriteLine("Заказа нет");
        else Console.WriteLine("Нужно привезти:" + _text);
    }
}
class Cashier : Employee
{
    public Cashier(IMediator mediator = null!) : base(mediator) { }
    
        public void ProcessPayment(string message)
        {
            Console.WriteLine("Кассир обрабатывает платеж: " + message);
        }
    
}
class Controller : IMediator
{
    private Officiant _officiant;
    private Cooker _cooker;
    private Deployment _deployment;
    private Cashier _cashier;

    public Controller(Officiant officiant, Cooker cooker, Deployment deployment, Cashier cashier)
    {
        _officiant = officiant;
        _officiant.SetMediator(this);

        _cooker = cooker;
        _cooker.SetMediator(this);

        _deployment = deployment;
        _deployment.SetMediator(this);

        _cashier = cashier;
        _cashier.SetMediator(this);
    }

    public void Notify(Employee employee, string msg)
    {
        if (employee is Officiant)
        {
            _cashier.ProcessPayment(msg);
        }
        else if (employee is Cooker)
        {
            _deployment.Explorer(msg);
        }
    }
}
