# Elixir

## Why Elixir?

There are two main reasons we will focus on. 

### Reason 1
The first reason has nothing to do with the Elixir language, but rather the Erlang Virtual Machine on which it runs. The Erlang Virtual Machine is built from the ground up to handle distributed and concurrent processing. It also makes use of lightweight Erlang processes (not to be confused with Operating System processes). It also follows the Actor Model, where the processes can be considered the actors. https://en.wikipedia.org/wiki/Actor_model

### Reason 2
Elixir is a modern functional language. It is concise, implements pattern matching at a function level, and generally is a great language to read once you get the hang of it. 

### Should everything be in Elixir?

Not necessarily. If it fits what you are doing and you need distributed highly concurrent code, then it will be great. Otherwise if you are already proficient in C# and C# will do the job well, then there's no need to switch. Both languages/technologies have their place.

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
				Triangle();
				break;
			case "rectangle":
				 Rectangle();
				break;
			case "square":
				Square();
				break;
			
		}
	}
	
	public static void Triangle()
	{
		Console.WriteLine("I'm a Triangle");
	}
	
	public static void Rectangle()
	{
		Console.WriteLine("I'm a Rectangle");
	}
	
	public static void Square()
	{
		Console.WriteLine("I'm a Square");
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

From these examples you can see how Elixir will automatically match the parameter to the correct function and run that function. With C# you need a handler method to direct the call to the correct method.
