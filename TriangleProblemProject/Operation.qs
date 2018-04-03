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
	//not used
	operation groverSearchFindEdges(adjMat : Int[][]) :(Int[])
	{
		body
		{
			
			mutable edgeArr = getAllEdges(adjMat);
			mutable retArr = new Int[Length(edgeArr)];
			using (input = Qubit[Length(edgeArr)])
			{
				HTransform(input);
				mutable complexity = ToDouble(Length(edgeArr));
				mutable iterations = Round(Sqrt(complexity));
				for(count in 0..iterations)
				{
				    oracleQueryAreEdges(edgeArr,input);
					GD(input); //Grover diffusion
				}
				set retArr = MeasureResults(input);
				ResetAll(input);
			}
			return (retArr);
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
	//a blackbox-like query to an oracle that sets to One if the input
	//is an edge
	operation oracleQueryAreEdges(edgeMat : Int[],qArray : Qubit[]) : ()
	{
		body
		{
			for(count in 0..(Length(edgeMat)-1))
			{
				if(edgeMat[count]== 1)
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
<<<<<<< Updated upstream
=======
	//an oracle query that sets to One if the inputted edge and point
	//form a triangle
>>>>>>> Stashed changes
	//TODO -returns index of triangle vertices
	operation findTriangle(adjMat : Int[][]) : (Int,Int,Int) 
	{
		body
		{
			mutable edges = getAllEdges(adjMat);
			for(count in 0..(Length(edges)-1))
			{
				if(edges[count]==1)
				{
					mutable loc = getLocationInMatrix(count,Length(adjMat[0]));
					let (numZeros, numOnes) =loc;
					mutable verOne = numOnes;
					mutable verTwo = numZeros;
					mutable arr = groverSearchFindThirdVer(adjMat,verOne,verTwo);
					if(isValidResult(arr))
					{
						mutable verThree = getIndexOfThirdVer(arr);
						return (verOne,verTwo,verThree);
					}
				}
			}
			return (-1,-1,-1);
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
	function getIndexOfThirdVer(inp : Int[]) : (Int)
	{
		for(count in 0..(Length(inp)-1))
		{
			if(inp[count] == 1)
			{
				return count;
			}
		}
		return -1;
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
<<<<<<< Updated upstream
	
=======

>>>>>>> Stashed changes
	function getLocationInMatrix(position : Int, size : Int):(Int, Int)
	{

		mutable nextLevel = size - 1;
		mutable nextGroupStart = 0;
		mutable increase = 0;
		mutable nextAdd = 1;
		repeat
		{
			set increase = increase + nextAdd;
			set nextAdd = nextAdd + 1;
			set nextGroupStart = nextGroupStart + nextLevel;
			set nextLevel = nextLevel - 1;
		}
		until(nextGroupStart > position)
		fixup
		{}

		mutable index = position + increase;
<<<<<<< Updated upstream
		mutable x = index / size;
		mutable y = index % size;
		
		return (x,y);
	}
	function verifyEndResult() : ()
	{
	

	}





	operation DatabaseOracleFromInts(markedElements : Int[],  markedQubit: Qubit, databaseRegister: Qubit[]) : ()
    {
        body {
            let nMarked = Length(markedElements);
            for (idxMarked in 0..nMarked - 1) {
                // Note: As X accepts a Qubit, and ControlledOnInt only 
                // accepts Qubit[], we use ApplyToEachCA(X, _) which accepts 
                // Qubit[] even though the target is only 1 Qubit.
				if (markedElements[idxMarked] == 1)
				{
					(ControlledOnInt(idxMarked, ApplyToEachCA(X, _)))(databaseRegister, [markedQubit]);
				}
            }

        }	
        adjoint auto
        controlled auto
        adjoint controlled auto
    }

	operation GroverStatePrepOracleImpl(markedElements : Int[], idxMarkedQubit: Int , startQubits: Qubit[]) : ()
    {
        body {
            let flagQubit = startQubits[idxMarkedQubit];
            let databaseRegister = Exclude([idxMarkedQubit], startQubits);

            // Apply oracle `U`
            ApplyToEachCA(H, databaseRegister);

            // Apply oracle `D`
            DatabaseOracleFromInts(markedElements, flagQubit, databaseRegister);

        }

        adjoint auto
        controlled auto
        adjoint controlled auto
    }

	function GroverStatePrepOracle(markedElements : Int[]) : StateOracle
    {
        return StateOracle(GroverStatePrepOracleImpl(markedElements, _, _));
    }

	function GroverSearch( markedElements: Int[], nIterations: Int, idxMarkedQubit: Int) : (Qubit[] => () : Adjoint, Controlled)
    {
        return AmpAmpByOracle(nIterations, GroverStatePrepOracle(markedElements), idxMarkedQubit);
    }

	operation ApplyGroverSearch( markedElements: Int[], nIterations : Int, nDatabaseQubits : Int) : (Result,Int) {
        body{
            // Allocate variables to store measurement results.
            mutable resultSuccess = Zero;
            mutable numberElement = 0;
            
            // Allocate nDatabaseQubits + 1 qubits. These are all in the |0〉
            // state.
            using (qubits = Qubit[nDatabaseQubits+1]) {
                
                // Define marked qubit to be indexed by 0.
                let markedQubit = qubits[0];

                // Let all other qubits be the database register.
                let databaseRegister = qubits[1..nDatabaseQubits];

                // Implement the quantum search algorithm.
                (GroverSearch( markedElements, nIterations, 0))(qubits);

                // Measure the marked qubit. On success, this should be One.
                set resultSuccess = M(markedQubit);

                // Measure the state of the database register post-selected on
                // the state of the marked qubit.
                let resultElement = MultiM(databaseRegister);

                set numberElement = PositiveIntFromResultArr(resultElement);

                // These reset all qubits to the |0〉 state, which is required 
                // before deallocation.
                ResetAll(qubits);
            }

            // Returns the measurement results of the algorithm.
            return (resultSuccess, numberElement);
        }

	}


	operation findTriangleNew(adjMat : Int[][]) : (Int,Int,Int) 
	{
		body
		{
			mutable edges = getAllEdges(adjMat);
			mutable repeats = 10;
			mutable iterations = 3;
			mutable edgeQubits = getNumOfQubits(Length(edges));
			mutable vertexQubits = getNumOfQubits(Length(adjMat));
			mutable resultCheck = One;
			for(count in 0..repeats)
			{
				mutable res = ApplyGroverSearch(edges, iterations, edgeQubits);
				let (resultSuccess, edge) = res;
			//Just need to be able to compare the Result type and this will work
			//	if (resultSuccess == One && edge >= 0)
			//	{
			//		mutable location = getLocationInMatrix(edge, Length(adj));
			//		let (x, y) =location;
			//		mutable vertexOne = x;
			//		mutable vertexTwo = y;
					
				//	for(count2 in 0..repeats)
				//	{
				//		mutable resTwo = ApplyGroverSearch(adj[vertexOne], iterations, vertexQubits);
				//		let (resultSuccessTwo, vertexThree) = resTwo;
				//		if (resultSuccessTwo == Result.One && vertexThree != vertexOne && vertexThree != vertexTwo && vertexThree >= 0 && adjMat[vertexTwo][vertexThree] == 1)
				//		{
				//			return(vertexOne, vertexTwo, vertexThree);
				//		}
				//	}
					

			//	}
			}
			return (-1,-1,-1);
		}
	}

	function getNumOfQubits(num : Int) : (Int)
	{
		mutable count = 1;
		mutable res = 0;
		repeat
		{
			set count = count * 2;
			set res = res + 1;
		}
		until(count >= num)

		fixup{}

		return res;
		
	}

}
