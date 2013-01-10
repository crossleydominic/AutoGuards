What are AutoGuards
===================

AutoGuards is a experimental library built on top of the C# compiler-as-a-service (Roslyn).
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



