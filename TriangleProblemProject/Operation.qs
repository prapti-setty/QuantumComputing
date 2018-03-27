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
    operation decipher(instr : Int[][]):(Int[])
	{
		
		body
		{
			mutable retArr = new Int[Length(instr)];
			repeat
			{
				set retArr = groverSearchFindThirdVer(instr,0,1);
				mutable res = isValidResult(retArr);
			}
			until(res == true)
			fixup
			{}
			return retArr;
			
		}
		
	}
	//TODO -attempts to find an edge present in a graph
	operation groverSearchFindEdges(adjMat : Int[][],N : Int) :(Int,Int)
	{
		body
		{
			mutable v1 = -1;
			mutable v2 = -1;
			
			return (v1,v2);
		}
	}
	//attempts to find third vertex of connected to an edge
	operation groverSearchFindThirdVer(adjMat : Int[][],v1:Int,v2:Int) :(Int[])
	{	
		body
		{
			mutable retArr = new Int[Length(adjMat)];
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
				set retArr = MeasureResults(input);
				ResetAll(input);
			}
			return retArr;
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
			for(count in 0..Length(adjMat[0])-1)
			{
				//ignoring the given edge
				if(count !=edge1 && count != edge2)
				{
					//set the qubit if it creates a triangle
					if(adjMat[edge1][count]==1 && adjMat[edge2][count]==1)
					{
						setQubitToOne(qArray[count]);
					}
					else
					{
						setQubitToZero(qArray[count]);
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
	operation setQubitToZero(q1: Qubit) : ()
    {
        body
        {
            let current = M(q1);
            if (current != Zero)
            {
                X(q1);
            }
        }
    }
	//Grover diffusion operator on qubits as: http://algassert.com/post/1706
	//Correct Qbit will be 1
	operation GD(qArr : Qubit[]) : ()
	{
		body
		{
			HTransform(qArr);
			(Controlled(Z))(Subarray(getControlledArray(qArr),qArr),qArr[0]);
			HTransform(qArr);
		
		}
	}
	//IBM's version of Grover Diffusion
	//Correct Qbit will be 0
	operation GDVer2(qArr : Qubit[]) : ()
	{
		body
		{
			HTransform(qArr);
			XTransform(qArr);
			H(qArr[0]);
			(Controlled(X))(Subarray(getControlledArray(qArr),qArr),qArr[0]);
			H(qArr[0]);
			XTransform(qArr);
			HTransform(qArr);
		}
	}

	//gets the control-bit array for the controlled z-gate
	operation getControlledArray(qArr : Qubit[]) : (Int[])
	{
		body
		{
			mutable retArr = new Int[Length(qArr)-1];
			for(count in 0..(Length(retArr)-1))
			{
				set retArr[count] = count + 1;
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
	operation XTransform(inp : Qubit[]) : ()
	{
		body
		{
			for(count in 0..(Length(inp)-1))
			{
				X(inp[count]);
			}
		}
	}
	operation TestFor(len : Int):(Int)
	{
		body
		{
		mutable total = 0;
		for(count in 0..len)
		{
			set total = total + count;
		}
		return total;
		}

	}
	//This Measures the QBit string  and encodes the results in an Int[]
	operation MeasureResults(QArr : Qubit[]) : (Int[])
	{
	    body
		{
		    mutable measVar = 0;
			mutable retArr = new Int[Length(QArr)];
			for(count in 0..(Length(QArr)-1))
			{
				let res = M(QArr[count]);
				if(res == Zero)
				{
					set measVar = 0;
				}
				else
				{
					set measVar = 1;
				}
				set retArr[count] = measVar;
		        
			}
			return retArr;
		}
	}
	operation getUniqueEdges(adjMat : Int[][]) : (Int[])
	{
		body
		{
		   mutable points = Round(Sqrt(ToDouble(Length(adjMat))));
		   mutable sizeOfArray = getTriangleNumber(points - 1);
		   mutable retArr = new Int[sizeOfArray];
		   for(count in 0..(Length(retArr)-1))
		   {
		   	
		   }
		   return retArr;
		   
		}
	}

	function getTriangleNumber(N : Int) : (Int)
	{
		mutable retVal = 1;
		for(count in 0..N)
		{
			set retVal = retVal + count;
		}
		return retVal;
		
	}
	operation isValidResult(inp : Int[]) : (Bool)
	{
		body
		{
			mutable resCount = 0;
			for(count in 0..(Length(inp)-1))
			{
				if(inp[count] == 1)
				{
				 set resCount = resCount + 1;
				 }
			}
			if(resCount == 1)
			{
			return true;
			}
			return false;
		}
	}
	
}
