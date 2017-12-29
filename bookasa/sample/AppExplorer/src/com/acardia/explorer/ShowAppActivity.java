package com.acardia.explorer;

import java.util.ArrayList;
import java.util.List;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.ComponentName;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.view.Window;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.BaseAdapter;
import android.widget.GridView;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

public class ShowAppActivity extends Activity implements Runnable, OnItemClickListener{
    
	GridView gv;
	ListView lv;
	
	boolean isGridView = true;
	
	private List<PackageInfo> packageInfos;
	
	private List<PackageInfo> userPackageInfos;

	private List<PackageInfo> showPackageInfos;

	
	private ProgressDialog pd;
	
	ImageButton ib_change_category;
	ImageButton ib_change_view;
	
	boolean isAllApp = true;
	
	private Handler mHandler = new Handler(){

		@Override
		public void handleMessage(Message msg) {
			// TODO Auto-generated method stub
			super.handleMessage(msg);
			
			gv.setAdapter(new GridViewAdapter(ShowAppActivity.this, showPackageInfos));
			lv.setAdapter(new ListViewAdapter(ShowAppActivity.this, showPackageInfos));
			

	        pd.dismiss();
		}
	};
	
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        
        this.requestWindowFeature(Window.FEATURE_NO_TITLE);
        
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
        		WindowManager.LayoutParams.FLAG_FULLSCREEN);
        
        setContentView(R.layout.show_app_grid);
        
        
        gv = (GridView) this.findViewById(R.id.gv_apps);
        gv.setOnItemClickListener(this);
                
        lv = (ListView) this.findViewById(R.id.lv_apps);
        lv.setOnItemClickListener(this);
        
        ib_change_category = (ImageButton) this.findViewById(R.id.ib_change_category);
        ib_change_view = (ImageButton) this.findViewById(R.id.ib_change_view);
        
        ib_change_view.setOnClickListener(new OnClickListener(){
        	
        	@Override       	
        	public void onClick (View v){
        		if (!isGridView)
    			{
        			gv.setVisibility(View.VISIBLE);
    				lv.setVisibility(View.GONE);
    				isGridView = true;
        			ib_change_view.setImageResource (R.drawable.grids);
    				//Toast.makeText(ShowAppActivity.this, "网格视图", Toast.LENGTH_LONG).show ();
        			
        			MyToast.myToastShow(ShowAppActivity.this, R.drawable.grids, "网格视图", Toast.LENGTH_LONG);
    			}
    			else
    			{
    				lv.setVisibility(View.VISIBLE);
    				gv.setVisibility(View.GONE);
        			ib_change_view.setImageResource (R.drawable.list);
    				isGridView = false;
    				//Toast.makeText(ShowAppActivity.this, "列表视图", Toast.LENGTH_LONG).show ();
    				MyToast.myToastShow(ShowAppActivity.this, R.drawable.list, "列表视图", Toast.LENGTH_LONG);
    			}
    			
        	}});
        
        ib_change_category.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				// TODO Auto-generated method stub
				if (isAllApp){	
					
					showPackageInfos = userPackageInfos;
        			isAllApp = false;
        			ib_change_category.setImageResource (R.drawable.user);
        			
        			Toast.makeText(ShowAppActivity.this, "用户安装的软件", Toast.LENGTH_LONG).show ();
        		}
        		else{
        			showPackageInfos = packageInfos;
        			isAllApp = true;
        			ib_change_category.setImageResource (R.drawable.all);
        			Toast.makeText(ShowAppActivity.this, "系统安装的软件", Toast.LENGTH_LONG).show ();
        		}
    			gv.setAdapter(new GridViewAdapter(ShowAppActivity.this, showPackageInfos));
    			lv.setAdapter(new ListViewAdapter(ShowAppActivity.this, showPackageInfos));
				
				
			}});
  
        pd = ProgressDialog.show(this, "请稍后...", "正在搜索你所安装的软件...", true, false);
        
        Thread t = new Thread (this);
        
        t.start();
    }

    private void init(){
    	
    }
    class ListViewAdapter extends BaseAdapter {


    	LayoutInflater inflater;
    	
    	List<PackageInfo> pkInfos;
    	
    	public ListViewAdapter (Context context, List<PackageInfo> packageInfos){
    		inflater = LayoutInflater.from(context);
    		
    		this.pkInfos = packageInfos;
    	}
    	
    	
		@Override
		public int getCount() {
			
			return pkInfos.size();
		}

		@Override
		public Object getItem(int arg0) {
			
			return pkInfos.get(arg0);
		}

		@Override
		public long getItemId(int position) {
					
			return position;
		}

		@Override
		public View getView(int position, View convertView, ViewGroup parent) {
			
			View view = inflater.inflate(R.layout.lv_item, null);
			
			TextView tv = (TextView) view.findViewById(R.id.lv_item_appname);
			TextView tvp = (TextView) view.findViewById(R.id.lv_item_packagename);
			
			ImageView iv = (ImageView) view.findViewById (R.id.lv_item_icon);
			
			PackageInfo packageInfo = pkInfos.get(position);
			
			PackageManager pm = getPackageManager();
			
			tv.setText(packageInfo.applicationInfo.loadLabel(pm));
			tvp.setText(packageInfo.applicationInfo.packageName);
			
			iv.setImageDrawable(packageInfos.get(position).applicationInfo.loadIcon(pm));
			
			return view;
		}
    	
    }

    class GridViewAdapter extends BaseAdapter {

    	LayoutInflater inflater;
    	
    	List<PackageInfo> pkInfos;
    	
    	public GridViewAdapter (Context context, List<PackageInfo> packageInfos){
    		inflater = LayoutInflater.from(context);
    		
    		this.pkInfos = packageInfos;
    	}
    	
    	
		@Override
		public int getCount() {
			
			return pkInfos.size();
		}

		@Override
		public Object getItem(int arg0) {
			
			return pkInfos.get(arg0);
		}

		@Override
		public long getItemId(int position) {
					
			return position;
		}

		@Override
		public View getView(int position, View convertView, ViewGroup parent) {
			
			View view = inflater.inflate(R.layout.gv_item, null);
			
			TextView tv = (TextView) view.findViewById(R.id.gv_item_appname);
			
			ImageView iv = (ImageView) view.findViewById (R.id.gv_item_icon);
			
			PackageInfo packageInfo = pkInfos.get(position);
			
			PackageManager pm = getPackageManager();
			tv.setText(packageInfo.applicationInfo.loadLabel(pm));
			
			iv.setImageDrawable(packageInfos.get(position).applicationInfo.loadIcon(pm));
			
			return view;
		}
    	
    }
	@Override
	public void run() {
		
    	packageInfos = this.getPackageManager().getInstalledPackages(PackageManager.GET_UNINSTALLED_PACKAGES | PackageManager.GET_ACTIVITIES );
    	showPackageInfos = packageInfos;
    	userPackageInfos = new ArrayList<PackageInfo>();
    	
    	for (int i = 0; i < packageInfos.size(); i ++){
    		PackageInfo temp = packageInfos.get(i);
    		
    		ApplicationInfo appinfo = temp.applicationInfo;
    		
    		boolean isUserApp = false;
    		
    		if ((appinfo.flags & ApplicationInfo.FLAG_UPDATED_SYSTEM_APP) != 0){
    			isUserApp = true;
    		}
    		else if ((appinfo.flags & ApplicationInfo.FLAG_SYSTEM) == 0) {
    			isUserApp = true;
    		}
    		if (isUserApp){
    			userPackageInfos.add(temp);
    		}
    	}
    	mHandler.sendEmptyMessage(0);
	}

	@Override
	public void onItemClick(AdapterView<?> arg0, View arg1, int position, long arg3) {
		
		final PackageInfo temp = showPackageInfos.get(position);
		
		AlertDialog.Builder builder = new AlertDialog.Builder(this);
		
		builder.setTitle("选项");
		
		builder.setItems(R.array.choice,  new DialogInterface.OnClickListener(){

			@Override
			public void onClick(DialogInterface arg0, int which) {
				
				switch (which)
				{
				case 0:
					String packageName = temp.packageName;
					ActivityInfo activityInfo = temp.activities[0];
					if (activityInfo == null)
					{
						Toast.makeText(ShowAppActivity.this, "找不到Activity", Toast.LENGTH_LONG).show();
					}
					else
					{	
						String activityName = activityInfo.name;
						
						Intent intent = new Intent ();
						
						intent.setComponent(new ComponentName (packageName, activityName));
						
						startActivity (intent);
					}
					
					break;
				case 1:
					break;
				case 2:
					break;
				}
			}
			
		});
		
		builder.setNegativeButton("取消", null);
		
		builder.create().show ();
	}
    
}