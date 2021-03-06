﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace AdvancedWorld
{
    internal sealed class CoupleAttributeEnumerator : IEnumerator<CoupleAttribute>
    {
        private IEnumerator enumerator;

        public CoupleAttributeEnumerator(Type humanType)
        {
            enumerator = (Attribute.GetCustomAttributes(humanType, typeof(CoupleAttribute)) as CoupleAttribute[]).GetEnumerator();
        }

        public CoupleAttribute Current => (CoupleAttribute)enumerator.Current;

        object IEnumerator.Current => enumerator.Current;

        public void Dispose() { }

        public bool MoveNext() => enumerator.MoveNext();

        public void Reset() => enumerator.Reset();
    }
}
