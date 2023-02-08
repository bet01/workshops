# Elixir

## Why Elixir?

There are two main reasons we will focus on. 

### Reason 1
The first reason has nothing to do with Elixir, but rather the Erlang Virtual Machine on which it runs.


### Reason 2


## Tools

Elixir online interactive:  https://replit.com/languages/elixir

C# online interactive:      https://dotnetfiddle.net/

## Let's compare

Pattern matching with function parameters

C#
```
using System;
					
public class Program
{
	public static void Main()
	{
		Shape("rectangle");
	}
	
	public static void Shape(string shapeType)
	{
		switch (shapeType)
		{
			case "triangle":
				Console.WriteLine("I'm a Triangle");
				break;
			case "rectangle":
				Console.WriteLine("I'm a Rectangle");
				break;
			case "square":
				Console.WriteLine("I'm a Square");
				break;
			
		}
	}
}
```

Elixir
```
defmodule Test do
   
  def shape(:triangle) do
    IO.puts("I'm a Triangle")
  end

  def shape(:rectangle) do
    IO.puts("I'm a Rectangle")
  end

  def shape(:square) do
    IO.puts("I'm a Square")
  end
end

Test.shape(:square)
```

