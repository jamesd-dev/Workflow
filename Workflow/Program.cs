using Workflow;

    AlphNumber a = new AlphNumber("a2vc");
    Console.WriteLine(a.AlphValue);
    Console.WriteLine(a.DecimalValue);
    a.Increment();
    Console.WriteLine(a.AlphValue);
    Console.WriteLine(a.DecimalValue);
    a.Decrement();
    Console.WriteLine(a.AlphValue);
    Console.WriteLine(a.DecimalValue);