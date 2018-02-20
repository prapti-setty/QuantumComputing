namespace Quantum.TriangleProblemProject
{
    open Microsoft.Quantum.Primitive;
    open Microsoft.Quantum.Canon;

    operation decipher(instr : Int[][]):(Int)
	{
		
		body
		{
			
			mutable one = instr[1][0];
			return (one);
		}
		
	}
	operation Swap():()
	{
		body{
			
		}
	}
}
