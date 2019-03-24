using System.Collections;
using System.Collections.Generic;

public enum DecorationItems { Hat, Moustache, }

public class DecorationItemList {
  public Hashtable DecorationList { get; private set; }

  public DecorationItemList()
  {
    DecorationList = new Hashtable();
  }
}
