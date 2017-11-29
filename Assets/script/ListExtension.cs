using System.Collections;
using System.Collections.Generic;

public static class ListExtension {
	public static List<Triple> uniq(this List<Triple> t_list){
		var uniqed = new List<Triple>(){};
		for(var i = 0; i < t_list.Count; i++){
			if (uniqed.IndexOf(t_list [i]) == -1) {
				uniqed.Add (t_list [i]);
			}
		}
		return uniqed;
	}
}
