//operation.qs by Isaac Walker
//grovers method as found at https://www.cs.cmu.edu/~odonnell/quantum15/lecture04.pdf
// For triangle finding, possibly use one of these papers:
// 2003: https://arxiv.org/abs/quant-ph/0310134
// 2014: https://arxiv.org/abs/1407.0085

namespace Quantum.TriangleProblemProject
{
    open Microsoft.Quantum.Primitive;
    open Microsoft.Quantum.Canon;
	open Microsoft.Quantum.Extensions.Math;
	open Microsoft.Quantum.Extensions.Convert;
	//TODO -For testing calls into QSharp
    operation decipher(instr : Int[][]):(Int)
	{
		
		body
		{
			
			mutable one = instr[1][0];
			using (input = Qubit[6])
			{
				HTransform(input);	
				ResetAll(input);
			}
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
	operation groverSearchFindThirdVer(adjMat : Int[][],v1:Int,v2:Int) :(Int)
	{	
		body
		{
			mutable ret = 1;
			using (input = Qubit[Length(adjMat[0])])
			{
				HTransform(input); //apply Hadamard Transform
				mutable complexity = ToDouble(Length(adjMat[0]));

				//we need the inputs to run through the oracle and diffusionOp √N times
				mutable iterations = Round(Sqrt(complexity)); 
				for(count in 0..iterations)
				{
					oracleQueryIfTriangle(adjMat,v1,v2,input); // query an edge through the oracle
					GD(input); //Grover diffusion
				}
				//H(input[0]);
			}
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
	operation oracleQueryIfTriangle(adjMat : Int[][],edge1 : Int,edge2: Int,qArray : Qubit[]) : ()
	{
		body
		{
			// loop through each vertex
			for(count in 1..Length(adjMat[0]))
			{
				//ignoring the given edge
				if(count-1 !=edge1 && count-1 != edge2)
				{
					//set the qubit if it creates a triangle
					if(adjMat[edge1][count-1]==1 && adjMat[edge2][count-1]==1)
					{
						setQubitToOne(qArray[count-1]);
					}
				}
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
			HTransform(qArr);
			(Controlled(Z))(Subarray(getControlledArray(qArr),qArr),qArr[0]);
			HTransform(qArr);
		
		}
	}
	//gets the control-bit array for the controlled z-gate
	operation getControlledArray(qArr : Qubit[]) : (Int[])
	{
		body
		{
			mutable retArr = new Int[Length(qArr)-1];
			for(count in 0..(Length(qArr)-1))
			{
				set retArr[count-1] = count + 1;
			}
			return retArr;
		}
	}
	// hadamard transform on qbit-array
	operation HTransform(inp : Qubit[]):()  //Tested
	{
		body
		{
			for(count in 0..(Length(inp)-1))
			{
				H(inp[count]);
			}
		}
	}

	operation getAllEdges(adjMat : Int[][]):(Int[])
	{
		body
		{
			mutable retArr = new Int[((Length(adjMat) * Length(adjMat)) - Length(adjMat)) / 2];		// nC2, or (n^2 - n) / 2
			mutable retArrIndex = 0;
			for (count in 0..(Length(adjMat) - 1))
			{
				for (count2 in count + 1..(Length(adjMat) - 1))
				{
					set retArr[retArrIndex] = adjMat[count][count2];
					set retArrIndex = retArrIndex + 1;
				}
			}

			return retArr;
		}
	}
}
