using System;
using System.Windows.Forms;
using SimPe.PackedFiles.Wrapper;

namespace pjse
{
	/// <summary>
	/// Summary description for IBhavPrimWiz.
	/// </summary>
	public abstract class ABhavPrimWiz
	{
		protected Instruction instruction = null;
		protected ABhavPrimWiz() {}

		protected ABhavPrimWiz(Instruction instruction) { this.instruction = instruction; }


		public abstract Panel bhavPrimWizPanel { get; }

		public abstract void Execute();
		public abstract Instruction Write();
		public abstract string OpcodeName(Bhav parent, ushort opcode, byte[] operands);
		public string OpcodeName(Bhav parent, ushort opcode)
		{
			return OpcodeName(parent, opcode, new byte[16]);
		}

		public string OpcodeName(Instruction instruction)
		{
			return OpcodeName(instruction.Parent, instruction.Opcode, instruction.Operands);
		}


		public override string ToString()
		{
			if (instruction != null) return OpcodeName(instruction);
			return null;
		}
	}

}
