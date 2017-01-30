#include "stdafx.h"

private ref class MedvedCpp sealed : MedvedFS::MedvedFS {
	public:
		void MeetMedved() override
		{
			System::Console::WriteLine("Hello from C++");
			MedvedFS::MedvedFS::MeetMedved();
		}
};