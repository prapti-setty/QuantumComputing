//operation.qs by Isaac Walker
//grovers method as found at https://www.cs.cmu.edu/~odonnell/quantum15/lecture04.pdf

namespace Quantum.TriangleProblemProject
{
    open Microsoft.Quantum.Primitive;
    open Microsoft.Quantum.Canon;
	open Microsoft.Quantum.Extensions.Math;
	//TODO -For testing calls into QSharp
    operation decipher(instr : Int[][]):(Int)
	{
		
		body
		{
			
			mutable one = instr[1][0];
			return (one);
		}
		
	}
	//TODO -attempts to find an edge present in a graph
	operation groverSearchFindEdge(adjMat : Int[][]) :(Int, Int)
	{
		body
		{
			return (1,2);
		}
	}
	//TODO -attempts to find third vertex of connected to an edge
	operation groverSearchFindThirdVer(adjMat : Int[][]) :(Int)
	{	
		body
		{
			mutable ret = -1;
			//using (input = Qubit[Length(adjMat[0])])
			//{
			//	HTransform(input); //apply Hadamard Transform
			//	mutable complexity = (Double)Length(adjMat[0]);
			//	mutable iterations = Sqrt(complexity);
			//}
			return ret;
		}
	
	}
	//TODO -returns index of triangle vertices
	operation findTriangle(adjMat : Int[][]) : (Int,Int,Int) 
	{
		body
		{
			
			return (1,2,3);
		}
	}
	//a blackbox-like query to an oracle that sets to One if the input
	//is an edge
	operation oracleQueryIfEdge(adjMat : Int[][],ver1: Int,ver2: Int,q : Qubit) : ()
	{
		body
		{
			if(adjMat[ver1][ver2]==1)
			{
				setQubitToOne(q);
			}
			
		}
	}
	
	//an oracle query that sets to One f the inputted edge and point
	//form a triangle
	operation oracleQueryIfTriangle(adjMat : Int[][],edge1 : Int,edge2: Int,vertex: Int,q : Qubit) : ()
	{
		body
		{
			if(adjMat[edge1][vertex]==1 && adjMat[edge2][vertex]==1)
			{
				setQubitToOne(q);
			}
			
		}
	}
	//sets the qubit to one
	operation setQubitToOne(q1: Qubit) : ()
    {
        body
        {
            let current = M(q1);
            if (current == Zero)
            {
                X(q1);
            }
        }
    }
	//TODO-Grover diffusion operator on qubits
	operation GD(qArr : Qubit[]) : ()
	{
		body
		{
		
		}
	}
	//TODO -get average amplitude µ
	operation getAverageAmplitude() : (Int)
	{
		body{
			mutable ret =1;
			return ret;
		}
	}
	// hadamard transform on qbit-array
	operation HTransform(inp : Qubit[]):()
	{
		body
		{
			for(count in 1..Length(inp))
			{
				H(inp[count]);
			}
		}
	}
}
