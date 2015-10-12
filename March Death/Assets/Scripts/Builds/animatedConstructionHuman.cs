﻿using UnityEngine;
using System.Collections;

using System;
using System.Collections.Generic;

public class animatedConstructionHuman : MonoBehaviour
{
    private Renderer andamio;
    private Renderer andamio2;

    private Renderer[] renderers;
    private List<Renderer> llista_1aFase;
    private List<Renderer> llista_2aFase;
    private List<Renderer> llista_3aFase;
    private Animation anim;

    void Awake() { }

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animation>();

        //animation = anim.GetClip("animAndamioHumanSawmill.anim");
        anim.Play("animAndamioHumanSawmill");

        //anim.Stop();
        llista_1aFase = new List<Renderer>();
        llista_2aFase = new List<Renderer>();
        llista_3aFase = new List<Renderer>();


        andamio2 = GameObject.Find("Andamio2").GetComponent<MeshRenderer>();
        andamio = GameObject.Find("Andamio").GetComponent<MeshRenderer>();

        llista_1aFase.Add(GameObject.Find("Suelo").GetComponent<MeshRenderer>());
        llista_1aFase.Add(GameObject.Find("Casa Part0").GetComponent<MeshRenderer>());
        llista_1aFase.Add(andamio);
        llista_1aFase.Add(andamio2);


        llista_2aFase.Add(GameObject.Find("Casa Part1").GetComponent<MeshRenderer>());
        llista_2aFase.Add(GameObject.Find("ListonSuelo").GetComponent<MeshRenderer>());
        llista_2aFase.Add(GameObject.Find("Troncos").GetComponent<MeshRenderer>());
        llista_2aFase.Add(GameObject.Find("EscaleraP").GetComponent<MeshRenderer>());


        llista_3aFase.Add(GameObject.Find("TroncosPeq").GetComponent<MeshRenderer>());
        llista_3aFase.Add(GameObject.Find("Terrat").GetComponent<MeshRenderer>());

        renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer i in renderers)
        {
            i.enabled = false;
        }
        foreach (Renderer i in llista_1aFase)
        {
            i.enabled = true;
        }

        StartCoroutine(Iniciar());

    }
    IEnumerator Iniciar()
    {
        yield return StartCoroutine(Accion1());
        //print("esto debe aparecer 30 segundos mas tarde");
        foreach (Renderer i in llista_2aFase)
        {
            i.enabled = true;
        }
        yield return StartCoroutine(Accion1());
        //print("esto debe aparecer 60 segundos mas tarde");
        foreach (Renderer i in llista_3aFase)
        {
            
            i.enabled = true;
        }
        yield return StartCoroutine(Accion1());
        //print("esto debe aparecer 90 segundos mas tarde");
        foreach (Renderer i in renderers)
        {
            i.enabled = true;
            andamio.enabled = false;
            andamio2.enabled = false;
        }
        anim.Play("animSawmillHuman");

    }

    private IEnumerator Accion1()
    {
        //print("iniciando accion 1");
        yield return new WaitForSeconds(10);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
