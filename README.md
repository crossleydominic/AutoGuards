What are AutoGuards
===================

AutoGuards is a experimental library built on top of the C# compiler-as-a-service ([Roslyn](http://msdn.microsoft.com/en-gb/roslyn)).
It allows a method's parameters to be marked up with custom attributes which will 
automatically emit guard clauses at compile time.

Example
=======

Instead of having to manually write guard clauses like this

	public void PerformSomeCalculation(SomeObject1 obj1, SomeObject2 obj2)
	{
		if(obj1 == null)
		{
			throw new ArgumentNullException("obj1");
		}
		
		if(obj2 == null)
		{
			throw new ArgumentNullException("obj2");
		}
	
		//...
	}
	
You can instead write the method like this 

	public void PerformSomeCalculation(
		[NotNull] SomeObject1 obj1, 
		[NotNull] SomeObject2 obj2)
	{
		//...
	}
	
The guard clauses are automatically emitted at compile time.

History
=======

The inspiration for this library came after thinking about the idea of interfaces and contracts.
An interface definition seems a little one-sided in that it only documents the obligations of the
implementor.  It says nothing about the obligations of the consumer. For example,
an interface may define a contract that says that implementors must provide an implementation for a
particular method with three arguments.  The interface doesn't document that the contract will only be fullfilled if the 
consumer promises to call that method with non-null arguments.

Once an interface method definitions had been annotated with the custom attributes then any implementors
would get the guard clauses inserted into their implementations for free at compile time.

What's missing
==============

A whole heap of stuff
- How should type conversions be handled?
- What happens for optional parameters?
- How are overridden methods handled?
- How are errors handled when using an AutoGuard on a mismatched type?

