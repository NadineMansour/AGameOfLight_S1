require 'test_helper'


class SchoolAdminsControllerTest < ActionController::TestCase
  include Devise::TestHelpers


  def setup
    @request.env["devise.mapping"] = Devise.mappings[:school_admin]
    sign_in school_admins(:one)
  end
  

  test "should get view verified students" do
    # start the request
  	get( :view_verified_students)
  	assert_response :success
  	assert_not_nil assigns(:current_admin)
  	assert_not_nil assigns(:students)
  	assert_nil assigns(:method)
    # we have only 2 verified students in the fixtures
    assert_equal 2, assigns(:students).count
  	assert_template :view_verified_students
  	assert_template layout: "layouts/application"
  end


  # test "should get view verified students but no students are there" do
  #   get( :view_verified_students)
  #   assert_response :success
  #   assert_not_nil assigns(:current_admin)
  #   assert_not_nil assigns(:students)
  #   assert_nil assigns(:method)
  #   # no verified stufdents of the school of the second school admin in the fixtures
  #   assert_equal 0, assigns(:students).count
  #   assert_template :view_verified_students
  #   assert_template layout: "layouts/application"
  # end


  #sort by grade asc
  test "should get view verified students and sort by grade asc" do
  	get( :view_verified_students, {'sort_method' => '1'})
  	assert_response :success
  	assert_not_nil assigns(:students)
  	assert_not_nil assigns(:current_admin)
  	assert_not_nil assigns(:method)
    assert_equal 2, assigns(:students).count
    assert_equal "5", assigns(:students).first.grade
  	assert_template :view_verified_students
  	assert_template layout: "layouts/application"
  end


  #sort by grade desc
  test "should get view verified students and sort by grade desc" do
    get( :view_verified_students, {'sort_method' => '2'})
    assert_response :success
    assert_not_nil assigns(:students)
    assert_not_nil assigns(:current_admin)
    assert_not_nil assigns(:method)
    assert_equal 2, assigns(:students).count
    assert_equal "7", assigns(:students).first.grade
    assert_template :view_verified_students
    assert_template layout: "layouts/application"
  end


  test "should get view students records" do
    get( :view_students_records)
    assert_response :success
    assert_not_nil assigns(:current_admin)
    assert_not_nil assigns(:students)
    assert_template :view_students_records
    assert_template layout: "layouts/application"
  end
  

  test "view requests as school admin" do
    get( :view_requests)
    assert_response :success
    assert_not_nil assigns(:current_admin)
    assert_not_nil assigns(:students)
    #onlu one student in the fixtures is unverified
    assert_equal 1, assigns(:students).count
    assert_template :view_requests
    assert_template layout: "layouts/application"
  end


  test "student should be removed from verified students" do
    assert_equal true, students(:two).verified
    students3 = Student.where(school: school_admins(:one).school, verified: true)
    assert_equal 2, students3.count
    put(:remove_verified_student, {'student_id' => students(:two).id })
    assert_not_nil assigns(:current_admin)
    assert_not_nil assigns(:student)
    students2 = Student.where(school: school_admins(:one).school, verified: true)
    assert_equal 1, students2.count
  end


  test "viewall subjects of school admin" do
    get (:view_school_subjects)
    assert_not_nil assigns(:subjects)
    assert_equal 1, assigns(:subjects).count
  end


end
