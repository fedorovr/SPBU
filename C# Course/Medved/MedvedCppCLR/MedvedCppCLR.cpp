#include "stdafx.h"
#include "MedvedCpp.h"

using namespace System;

int main(array<System::String ^> ^args)
{
	MedvedCpp^ medved = gcnew MedvedCpp();
	medved->MeetMedved();
	return 0;
}
