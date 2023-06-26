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

### Pattern matching with function parameters

*C#* does not have pattern matching at a method parameter level, so we need to use some other mechanism, such as a switch statement seen below:
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

*Elixir* can pattern match at a functino parameter level, removing the need for a case statement
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

### Switch/Case Statements

In recent years *C#* has brought in some functional elements, such as pattern matching with switch statements.
```
using System;
					
public class Program
{
	public static void Main()
	{
		ProcessPerson(new Person
					  {
						  FirstName = "Bob",
						  LastName = "Smith",
						  Age = 28
					  });
		
		ProcessPerson(new Person
					  {
						  FirstName = "John",
						  LastName = "Smith",
						  Age = 28
					  });
		
		ProcessPerson(new Person
					  {
						  FirstName = "John",
						  LastName = "Smith",
						  Age = 31
					  });
		
		ProcessPerson(new Person
					  {
						  FirstName = "John",
						  LastName = "Smith",
						  Age = 47
					  });
	}
	
	public class Person
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Age { get; set; }		
	}
	
	public static void ProcessPerson(Person person)
	{
		string message = person switch
		{
			{ FirstName: "Bob" } => "Hi Bob! Long time!",
			{ Age: int age } when age > 40 => $"You are older than 40, you are {age}",
			{ Age: > 30 } => "You are older than 30",			
			{ FirstName: string firstName }  => $"Hi {firstName}"
		};
		
		Console.WriteLine(message);
	}
}
```

The *Elixir* version is seen below
```
defmodule ProcessPerson do

  def process(person) do

    message = case person do
      %{ first_name: "Bob" } -> "Hi Bob! Long time!"
      %{ age: age } when age > 40 -> "You are older than 40, you are #{age}"
      %{ age: age } when age > 30 -> "You are older than 30"
      %{ first_name: first_name } -> "Hi #{first_name}"
    end	

    IO.puts(message)

  end

end

ProcessPerson.process(%{						
  first_name: "Bob",
  last_name: "Smith",
  age: 28
})
		
ProcessPerson.process(%{						  
  first_name: "John",
  last_name: "Smith",
  age: 28
})
		
ProcessPerson.process(%{						  
  first_name: "John",
  last_name: "Smith",
  age: 31
})
		
ProcessPerson.process(%{						  
  first_name: "John",
  last_name: "Smith",
  age: 47
})
```

### Piping

In *C#* you may have come across this, notice how 3 method calls are wrapped around the person object:
```
using System;
using System.Text.Json;
					
public class Program
{
	public static void Main()
	{
		var person = new Person
		{
			FirstName = "Bob",
			LastName = "Smith",
			Age = 28
		};
		
		Console.WriteLine(JsonSerializer.Serialize(AgePerson(person)));		
	}
	
	public class Person
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Age { get; set; }		
	}
	
	public static Person AgePerson(Person person)
	{
		person.Age += 1;
		return person;
	}
}
```

In *Elxir*, and many functional languages, we have the pipe operator `|>` which takes the result of the previous function and passes it to the next function as it's first parameter. This gives us a clear and easy to read call chain.
```
defmodule ProcessPerson do

  def process(person) do
    person
    |> age_person()
    |> IO.inspect()
  end
  
  defp age_person(person) do
    Map.update!(person, :age, &(&1 + 1))
  end

end

ProcessPerson.process(%{						
  first_name: "Bob",
  last_name: "Smith",
  age: 28
})
```

