using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cupVarGUI : MonoBehaviour {

	[SerializeField]
	private CupController cup;
    [SerializeField]
    private Text staticVars;
	[SerializeField]
	private Text formula;

    /*NONFIXED VARS*/
    [SerializeField]
    float temp; // T (in Kelvins) 
    [SerializeField]
    float time; // t where 1 minute = 1 day
    [SerializeField]
    float L1; // L1
    [SerializeField]
    float Y_A0;
    [SerializeField]
    float Y_AL;


    private void Awake() {
        
    }

    private void Update() {
        formula.text = "C_A(liq)((" + cup.getL1().ToString("0.00000") + ")^2 - (L_0)^2    =    (-PD_ABln((1-" + cup.getY_A0().ToString("0.00000")
           + ")/1-" + cup.getY_AL().ToString("0.00000") + "))" + cup.getTime().ToString("0.00000") 
           + "\n_____________________         _________________________________" 
           + "\n          2                                                R" + cup.getTemp().ToString("0.00000") 
           + "\n\nY_AL = r_h(Y_AO)\n" + cup.getY_AL().ToString("0.00000") + " = " + cup.getr_h().ToString("0.00000")
           + "(" + cup.getY_A0().ToString("0.00000") + ")";



    }


}
