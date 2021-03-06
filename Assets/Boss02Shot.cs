﻿using UnityEngine;
using System.Collections;

public class Boss02Shot : MonoBehaviour {

	public GameObject explosion;
	public float amplitude = 0.01f;		// 振幅
	private int frameCnt = 0;			// フレームカウント
	private Vector3 _dir;
	private float _speed;
	public GameObject SmallBoss02;

	// Use this for initialization
	void Start () {
		// スピードをランダムにする
		float x = Random.Range (-3, 3);
		float y = Random.Range (-3, 3);
		float z = Random.Range (-3, 3);
		gameObject.transform.position = new Vector3 (x, y, z);
		_speed = Random.Range (1, 9) / 10f;
		// 方向をランダムにする
		x = Random.Range (-3, 3);
		y = Random.Range (-3, 3);
		z = Random.Range (-3, 3);
		_dir = new Vector3 (x, y, z);

		//現後一定時間で自動的に消滅させる
		Destroy(gameObject, 4.0F);
	}
	
	// Update is called once per frame
	void Update () {
		//弾を前進させる
		gameObject.transform.Translate (_dir * _speed);
	}

	private void OnCollisionEnter(Collision collider) {
		
		//プレイヤーと衝突したら爆発して消滅する
		if (collider.gameObject.tag == "Player") {
			Destroy (gameObject);
			Instantiate (explosion, transform.position, transform.rotation);
			if (Random.Range (0, 6) == 0) {
				Instantiate (SmallBoss02, transform.position, transform.rotation);
			}
		} else if (collider.gameObject.tag == "Shot") {
			Destroy (gameObject);
			Instantiate (explosion, transform.position, transform.rotation);
			if (Random.Range (0, 6) == 0) {
				Instantiate (SmallBoss02, transform.position, transform.rotation);
			}
		}
	}

	IEnumerator Boss02ShotCoroutine () {
		frameCnt += 1;
		if (10000 <= frameCnt) {
			frameCnt = 0;
		}
		if (0 == frameCnt % 2) {
			// 上下に振動させる
			float posYSin = Mathf.Sin (2.0f * Mathf.PI * (float)(frameCnt % 200) / (200.0f - 1.0f));
			iTween.MoveAdd (gameObject, new Vector3 (0, amplitude * posYSin, 0), 0.0f);
			yield return new WaitForSeconds (0.1f);
		}
	}
}
