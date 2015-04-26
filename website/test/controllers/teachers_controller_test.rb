require 'test_helper'


class TeachersControllerTest < ActionController::TestCase
  include Devise::TestHelpers


  def setup
    @request.env["devise.mapping"] = Devise.mappings[:teacher]
    sign_in teachers(:one)
  end


  test "should get view game records" do
    get( :view_game_records)
    assert_response :success
    assert_not_nil assigns(:current_teacher)
    assert_not_nil assigns(:students)
    assert_template :view_game_records
    assert_template layout: "layouts/application"
  end


   test "should get view verified students" do
    # start the request
    get( :view_school_verified_students)
    assert_response :success
    assert_not_nil assigns(:current_teacher)
    assert_not_nil assigns(:students)
    assert_nil assigns(:method)
    assert_nil assigns(:order)
    # we have only 2 verified students in the fixtures
    assert_equal 2, assigns(:students).count
    assert_template :view_school_verified_students
    assert_template layout: "layouts/application"
  end


  test "teacher can view verified students and sort by grade asc" do
    get( :view_school_verified_students, {'sort_method' => '1', 'order_method' => '1'})
    assert_response :success
    assert_not_nil assigns(:students)
    assert_not_nil assigns(:current_teacher)
    assert_not_nil assigns(:method)
    assert_not_nil assigns(:order)
    assert_equal 2, assigns(:students).count
    assert_equal "5", assigns(:students).first.grade
    assert_template :view_school_verified_students
    assert_template layout: "layouts/application"
  end

  test 'view verified teachers' do
     get( :view_verified_teachers )
    assert_response :success
    assert_not_nil assigns(:teachers)
    assert_not_nil assigns(:current_admin)
    assert_equal 2, assigns(:teachers).count
    assert_template :view_verified_teachers
    assert_template layout: "layouts/application"

  end

  test "teacher should be removed from verified teachers" do
    assert_equal true, teachers(:three).verified
    teachers3 = Teacher.where(school: school_admins(:one).school, verified: true)
    assert_equal 2, students3.count
    put(:remove_verified_student, {'student_id' => teachers(:three).id })
    assert_not_nil assigns(:current_admin)
    assert_not_nil assigns(:teacher)
    teachers2 = Teacher.where(school: school_admins(:one).school, verified: true)
    assert_equal 1, teachers2.count
  end


  

end