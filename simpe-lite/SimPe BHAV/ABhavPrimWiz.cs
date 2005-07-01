using System;
using SimPe.PackedFiles.Wrapper;

namespace pjse
{
	/// <summary>
	/// Summary description for IBhavPrimWiz.
	/// </summary>
	public abstract class ABhavPrimWiz
	{
		public abstract System.Windows.Forms.Panel bhavPrimWizPanel();
		public abstract void Execute(Instruction instruction);
		public abstract Instruction Write();
		public abstract string OpcodeName(Bhav parent, ushort opcode, byte[] operands);
		public string OpcodeName(Bhav parent, Instruction instruction)
		{
			return OpcodeName(parent, instruction.Opcode, instruction.Operands);
		}
	}

}
