using System.Collections;
using System.Collections.Generic;
using System;

public class Mine
{
	public static Field field;
	public Mine(int x_max, int y_max , int z_max){
		Mine.field = new Field (x_max,y_max,z_max);
	}
}


public class Field
{
	// publicにしないと下のLengthが使えなくなるため
	public X_axis[] x;
	//field[x][y][z]のような参照方法にするため
	public X_axis this[int i]
	{
		get{ return x[i]; }
		set{ x [i] = value;}
	}
	public Field( int x_max, int y_max, int z_max){

		x = new X_axis[x_max];
		for(int i = 0; i < x_max; i++){
			x[i] = new X_axis (x_max,y_max,z_max);
		}
	}
	public int Length{
		get{
			return x.Length;
		}
	}
}

public class X_axis
{
	
	public Y_axis[] y;

	public Y_axis this[int i]
	{
		get{ return y[i]; }
		set{ y [i] = value;}
	}
	public X_axis( int x_max, int y_max, int z_max){
		y = new Y_axis[y_max];
		for(int i = 0; i < y_max; i++){
			y[i] = new Y_axis (x_max,y_max,z_max);
		}
	}
	public int Length{
		get{
			return y.Length;
		}
	}
}
public class Y_axis
{
	private Pair[] z;
	public Pair this[int i]
	{
		get{ return z[i]; }
		set{ z[i] = value;}
	}
	public Y_axis( int x_max, int y_max, int z_max){
		z = new Pair[z_max];
		for(int i = 0; i < z_max; i++){
			z[i] = new Pair(0,0);
		}
	}


}
/* c++のpairみたいなもの
 * firstにはBombなどの動かない情報を入れ、
 * secondに開いているかの情報を入れる 0未開封 1開封済み 2 旗を立て済み 3クエスチョン
 * secondに入れるのはStateEnumの列挙体
 */ 
public class Pair //: IEquatable<Pair>
{
	private int[] p;
	public Pair(int i_first, int i_second){
		p = new int[2];
		first = i_first;
		second = i_second;
	}
	public int first{
		get{
			return p [0];
		}
		set{
			p [0] = value;
		}
	}
	public int second
	{
		get{
			return p [1];
		}
		set{
			p [1] = value;
		}
	}
	public static bool operator ==(Pair p1,Pair p2){
		return (p1.first == p2.first && p1.second == p2.second);
	}

	public static bool operator !=(Pair p1,Pair p2){
		return !(p1.first == p2.first && p1.second == p2.second);
	}
	//indexofに対応できるように
	public override bool Equals(object obj){
		// null 型比較
		if(obj == null || this.GetType() != obj.GetType()){
			return false;
		}
		var other = (Pair)obj;
		return (this.first == other.first && this.second == other.second);
	}

}
// pairの三つ版
public class Triple // : IEquatable<Triple>
{
	private int[] t;
	public Triple(int i_first, int i_second,int i_third){
		t = new int[3];
		first = i_first;
		second = i_second;
		third = i_third;
	}
	public int first{
		get{
			return t [0];
		}
		set{
			t [0] = value;
		}
	}
	public int second
	{
		get{
			return t [1];
		}
		set{
			t [1] = value;
		}
	}
	public int third
	{
		get{
			return t [2];
		}
		set{
			t [2] = value;
		}
	}
	public static bool operator ==(Triple t1,Triple t2){
		return (t1.first == t2.first && t1.second == t2.second && t1.third == t2.third);
	}

	public static bool operator !=(Triple t1,Triple t2){
		return !(t1.first == t2.first && t1.second == t2.second && t1.third == t2.third);
	}
	public override bool Equals(object obj){
		// null 型比較
		if(obj == null || this.GetType() != obj.GetType()){
			return false;
		}
		var other = (Triple)obj;
		return (this.first == other.first && this.second == other.second && this.third == other.third);
	}
}